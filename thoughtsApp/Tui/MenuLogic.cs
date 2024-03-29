﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace thoughtsApp.Tui
{
    /// <summary>
    /// if menu needs additional logic, here's the place for functions that extend it
    /// </summary>
    public static class MenuLogic
    {
        public static bool DoesTxtFileExist(string path, out string result)
        {
            string text = "";
            Console.Clear();
            try
            {
                if (File.Exists(path)) // idk if such thing shouldn't be thrown into FileManager
                {
                    using (var reader = new StreamReader(path))
                    {
                        text = reader.ReadToEnd();
                    }
                    result = text;
                    return true;
                }
                else
                {
                    Console.WriteLine("Corrupted file or bad extension.\nPress any key to continue...");
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("Error during reading .txt file\nPress any key to continue...");
                Console.ReadLine();
            }
            result = "";
            return false;
        }
        public static async Task<(Menu menu, List<(string Name, string Id)> infos)> GetMenuAndFoldersInfo()
        {
            var folderInfo = await FileManager.getCurrentFolders();
            var options = new List<(string functionArgument,string options)>();
            for (int i = 0; i < folderInfo.Count; i++)
            {
                options.Add(("",$"{i + 1}. {folderInfo[i].name}"));
            }
            options.Add(("","Go back"));
            return (new Menu(@"Choose folder", emptyAction, options.ToArray()), (folderInfo));
        }
        public static async Task<(Menu menu,int length)> GetPhraseMenu(Action<string> action,string phrase, List<(string Name, string Id)>infos) 
        {
            List<(string functionArgument, string option)> options = new List<(string functionArgument, string option)>();
            List<(string Name, string Id)> copy = new List<(string Name, string optionId)>(infos); //we can't iterate over original list if we want to remove items from it
            foreach (var note in copy)
            {
                bool phraseInNote = false;
                string actionArgument = "";
                var noteText = await FileManager.GetFileText(note.Id);
                var sentences = noteText.Split('.').ToList();
                foreach (var sentence in sentences)
                {
                    if (sentence.Contains(phrase))
                    {
                        actionArgument += $"- {sentence.Trim()}\n";
                        if (!phraseInNote)
                        {
                            phraseInNote = true;
                        }
                    }
                }
                if (phraseInNote)
                {
                    options.Add((actionArgument, note.Name));
                }
                else
                    infos.Remove(note);
            }
            options.Add(("", "Go back"));
            Menu menu = new Menu(phrase, action, options.ToArray());
            return (menu,options.Count);
        }
        public static async Task<(Menu menu, List<(string Name, string Id)> infos)> GetMenuAndNotesInfo()
        {
            var notesInfo = await FileManager.GetNotesInfoFromDrive();
            var options = new List<(string functionArgument, string options)>();
            for (int i = 0; i < notesInfo.Count; i++)
            {
                options.Add(("",$"{i + 1}. {notesInfo[i].name}"));
            }
            options.Add(("","Go back"));
            return (new Menu("List of current notes:",emptyAction,options.ToArray()), (notesInfo));     
        }
        public static async Task ViewExpressionSentences(string phrase) 
        {
            //1. Funkcja która pobiera wszystkie notatki, i od razu po pobraniu jaiejkowlwiek sprawdza, czy dany expression się w niej znajduje
            //2. Wypisujemy zdania które w ramach tej jednej notatki zawierają daną frazę, ta fraza ma być kolorowa
            //3. 
        }
        public static void FilesLookup((string name, string id) fileInfo, string text)
        {
            Console.WriteLine($"Notatka: {fileInfo.name}\n");
            Console.WriteLine(text);
        }
        public static async Task GetExpressionSentences(string phrase, string noteId) 
        {
            var text = await FileManager.GetFileText(noteId);
            var sentences = text.Split('.').ToList();
            List<string> exprSentences = new List<string>();
            foreach (var sentence in sentences)
            {
                if (sentence.Contains(phrase))
                {
                    exprSentences.Add(sentence);
                }
            }
        }
        public static async Task RandomFileViewer()
        {
            var list = FileManager.GetNotesInfoFromDrive();
            while (!list.IsCompleted)
                Visuals.WaitingAnimation("Choosing a perfect note");
            int note = new Random().Next(0, list.Result.Count - 1);
            FileViewer viewer = new FileViewer(list.Result, note,"");
            viewer.Run();
        }
        public static void ReadSentenceWithColoredExpression(string sentence, string expression)
        {
            int startIndex = 0;
            while (startIndex < sentence.Length)
            {
                int wordIndex = sentence.IndexOf(expression, startIndex);
                if (wordIndex != -1)
                {
                    Console.Write(sentence.Substring(startIndex, wordIndex - startIndex));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(expression);
                    Console.ResetColor();
                    startIndex = wordIndex + expression.Length;
                }
                else
                {
                    Console.Write(sentence.Substring(startIndex));
                    break;
                }
            }
        }
        public static void emptyAction(string text) {}
    }
}
