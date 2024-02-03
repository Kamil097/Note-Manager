using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static thoughtsApp.Tui.Visuals;
using static System.Console;
using System.ComponentModel;
using System.Collections.Specialized;

namespace thoughtsApp.Tui
{
    /// <summary>
    /// application frontend
    /// </summary>
    public class MenuTemplateLogic
    {
        public async void RunInitialActionMenu()
        {
            var foldersInformation = Task.Run(()=>MenuLogic.PrepareFolderListMenu());
            while (!foldersInformation.IsCompleted)
                WaitingAnimation("Downloading folder info");
            var result = await foldersInformation; 

            var menu = MenuTemplates.InitialActionMenu;
            int mainOption = menu.Run() + 1;
            Clear();
            switch (mainOption)
            {
                case 1:
                    int option1 = result.menu.Run();
                    if (option1 == result.infos.Count)
                        break;
                    FileConfig.folderId = result.infos[option1].Id;
                    FileConfig.folderName = result.infos[option1].Name;
                    RunMainMenu();
                    break;
                case 2:
                    WriteLine("Insert folder name:");
                    var createTask = FileManager.CreateNewFolder(ReadLine().Trim().Replace(" ", "_"));
                    while (!createTask.IsCompleted)
                        Visuals.WaitingAnimation("Creating folder");
                    break;
                case 3:
                    int option2 = result.menu.Run();
                    var deleteTask = FileManager.DeleteNoteFromGoogleDrive(result.infos[option2].Id);
                    while (!deleteTask.IsCompleted)
                        Visuals.WaitingAnimation("Deleting folder");
                    break;
                case 4:
                    return;
            }
            RunInitialActionMenu();
        }
        public async void RunMainMenu()
        {
            var menu = MenuTemplates.MainMenu;
            int mainOption = menu.Run() + 1;
            Clear();
            switch (mainOption)
            {
                case 1:
                    WriteLine("Write your thought");
                    await FileManager.SendNewNote(ReadLine(), "note");
                    await Task.Delay(1000);
                    break;
                case 2:
                    RunInsertFromFileMenu();
                    break;
                case 3:
                    var notesInformation = Task.Run(()=>MenuLogic.GetMenuAndNotesInfo());
                    while (!notesInformation.IsCompleted)
                        WaitingAnimation("Downloading notes information");
                  
                    var result = await notesInformation;
                    int option1 = result.menu.Run();
                    if (option1 == result.infos.Count)
                        break;
                    MenuLogic.FileViewer(result.infos,option1);     
                    //utworzyć fileViewer jako klase podobną do menu, bedzie git
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
            RunMainMenu();
        }
       
        public async void RunInsertFromFileMenu()
        {
            var menu = MenuTemplates.InsertFromFileMenu;
            int option = menu.Run()+1;
            switch(option)
            {
                case 1:
                    WriteLine("Insert path to file you'd like to upload:");

                    string text = "";
                    if(!MenuLogic.DoesTxtFileExist(ReadLine(), out text))
                        break;

                    WriteLine("Insert file name:");
                    string name = FileManager.GetDateTimeName(ReadLine());

                    var task = Task.Run(()=>FileManager.SendNewNote(text, name));
                    while (task.IsCompleted)
                        WaitingAnimation("Uploading file");
                    await task;
                    break;
                case 2:
                    WriteLine("Not implemented (if ever xd)");
                    break;
            }
        }
    }
}
