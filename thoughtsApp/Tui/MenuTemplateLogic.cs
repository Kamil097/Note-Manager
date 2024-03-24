using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static thoughtsApp.Tui.Visuals;
using static System.Console;
using System.ComponentModel;
using System.Collections.Specialized;
using thoughtsApp;
using Newtonsoft.Json.Converters;
using System.Security.AccessControl;

namespace thoughtsApp.Tui
{
    /// <summary>
    /// application frontend
    /// </summary>
    public class MenuTemplateLogic
    {
        public async Task RunInitialActionMenu()
        {     
            var foldersInformation = MenuLogic.GetMenuAndFoldersInfo();
            while (!foldersInformation.IsCompleted)
                WaitingAnimation("Downloading folder info");

            var result = foldersInformation.Result; 
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
                    string name = MenuLogic.InsertText(true);
                    if (string.IsNullOrEmpty(name))
                        break;
                    var createTask = FileManager.CreateNewFolder(name);
                    while (!createTask.IsCompleted)
                        WaitingAnimation("Creating folder");
                    break;
                case 3:
                    int option2 = result.menu.Run();
                    if (option2 == result.infos.Count)
                        break;
                    var deleteTask = FileManager.DeleteNoteFromGoogleDrive(result.infos[option2].Id);
                    while (!deleteTask.IsCompleted)
                        WaitingAnimation("Deleting folder");
                    break;
                case 4:
                    return;
            }
            RunInitialActionMenu();
        }
        public async Task RunMainMenu()
        {
            var menu = MenuTemplates.MainMenu;
            int mainOption = menu.Run() + 1;
            Clear();
            switch (mainOption)
            {
                case 1:
                    WriteLine("Write your thought");
                    var noteText = MenuLogic.InsertText(false);
                    if (string.IsNullOrEmpty(noteText))
                        break;
                    var task = FileManager.SendNewNote(noteText, "note");
                    while (!task.IsCompleted)
                        WaitingAnimation("Sending new note");
                    break;
                case 2:
                    RunInsertFromFileMenu();
                    break;
                case 3:
                    RunNotesInfoMenu();
                    break;
                case 4:
                    MenuLogic.RandomFileViewer();
                    break;
                case 5:
                    RunExpressionMenu();
                    break;
                case 6:
                    return;
            }
            RunMainMenu();
        }
       
        public async Task RunNotesInfoMenu() 
        {
            var notesInformation = MenuLogic.GetMenuAndNotesInfo();
            while (!notesInformation.IsCompleted)
                WaitingAnimation("Downloading notes information");

            var result = notesInformation.Result;
            int option = result.menu.Run();
            if (option == result.infos.Count)
                return;
            FileViewer viewer = new FileViewer(result.infos, option, "");
            viewer.Run();
            RunNotesInfoMenu();
        }
        public async Task RunInsertFromFileMenu()
        {
            var menu = MenuTemplates.InsertFromFileMenu;
            int option = menu.Run() +1;
            switch(option)
            {
                case 1:
                    WriteLine("Insert path to file you'd like to upload:");

                    string text = "";
                    if(!MenuLogic.DoesTxtFileExist(ReadLine(), out text))
                        break;

                    WriteLine("Insert file name:");
                    string name = FileManager.GetDateTimeName(ReadLine());

                    var task = FileManager.SendNewNote(text, name);
                    while (task.IsCompleted)
                        WaitingAnimation("Uploading file");
                    break;
                case 2:
                    WriteLine("Not implemented (if ever xd)");
                    break;
            }
        }
        public async Task RunExpressionMenu()
        {
            var notesInformation = MenuLogic.GetMenuAndNotesInfo();
            while (!notesInformation.IsCompleted)
                WaitingAnimation("Downloading notes information");

            WriteLine("Wprowadź fraze do wyszukania:");
            string phrase = ReadLine();
            var phraseMenuTask = MenuLogic.GetPhraseMenu(display,phrase,notesInformation.Result.infos);
            while (!phraseMenuTask.IsCompleted)
                WaitingAnimation("Przeszukiwanie notatek");
            var result = phraseMenuTask.Result;
            var option = result.menu.Run();
            if (option == result.length)
                return;
            FileViewer viewer = new FileViewer(notesInformation.Result.infos, option,phrase);
            viewer.Run();
        }
        public static void display(string text) { WriteLine($"{text.Trim()}"); }
    }
}