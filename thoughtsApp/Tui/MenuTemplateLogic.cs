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
        public async void RunMainMenu()
        {
            var menu = MenuTemplates.MainMenu;
            int option = menu.Run() + 1;
            Clear();
            switch (option)
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

                    var task = MainViewLogic.UploadFileAsync(FileConfig.folderId, name, text);
                    while (task.IsCompleted)
                        WaitingAnimation("Uploading file");
                    break;
                case 2:
                    //.GetTextFromFile(); 
                    WriteLine("Not implemented (if ever xd)");
                    break;
            }
        }
    }
}
