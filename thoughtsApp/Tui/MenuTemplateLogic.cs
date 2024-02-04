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
                    var createTask = FileManager.CreateNewFolder(ReadLine().Trim().Replace(" ", "_"));
                    while (!createTask.IsCompleted)
                        WaitingAnimation("Creating folder");
                    break;
                case 3:
                    int option2 = result.menu.Run();
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
                    var task = FileManager.SendNewNote(ReadLine(), "note");
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
                    break;
                case 5:
                    RunExpressionMenu();
                    break;
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
            FileViewer viewer = new FileViewer(result.infos, option);
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
            List<(string functionArgument, string option)> options = new List<(string functionArgument, string option)>();

            var notesInformation = MenuLogic.GetMenuAndNotesInfo();
            while (!notesInformation.IsCompleted)
                WaitingAnimation("Downloading notes information");

            WriteLine("Wprowadź fraze do wyszukania:");
            string phrase = ReadLine();

            foreach (var note in notesInformation.Result.infos)
            {
                bool phraseInNote = false;
                string actionArgument = "";
                var noteText = FileManager.GetFileText(note.Id);
                var sentences = noteText.Result.Split('.').ToList();
                foreach (var sentence in sentences)
                {
                    if (sentence.Contains(phrase))
                    {
                        actionArgument += $" - {sentence} \n";
                        if (!phraseInNote)
                        {
                            options.Add((actionArgument, note.Name));
                            Menu menu = new Menu(phrase, display, options.ToArray());
                            menu.Run();
                            phraseInNote = true;
                        }
                        else
                        {
                            options.Add((actionArgument, note.Name));
                            Menu menu = new Menu(phrase, display, options.ToArray());
                            menu.Run();
                        }
                        //refresh menu
                    }
                }
            }
        }
        public static void display(string text) { WriteLine($"{text}"); }
        public async Task<string> GetPhraseSentence(string phrase,string noteId)
        {
            var noteText = await FileManager.GetFileText(noteId);
            return "";

        }
    }
    
}


//case mode.FindExpression:
//   WriteLine("Insert phrase you want to find.");s
//   string phrase = ReadLine();
//   foreach (var note in NoteInformation)
//   {
//       var noteText = await FileManager.GetFileText(note.id);
//       var sentences = noteText.Split('.').ToList();
//       foreach (var sentence in sentences)
//       {
//           if (sentence.Contains(phrase))
//           {
//           }
//       }
//   }
//   //List<(string option, Action function)> options = new List<(string option, Action function)>();
//   //options.Add(($"", dupa));
//   //Menu menu = new Menu("asdf", ("asdf", dupa); 

//   break;