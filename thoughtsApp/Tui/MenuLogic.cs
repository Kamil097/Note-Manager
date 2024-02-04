using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
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
            return (new Menu("Choose folder",emptyAction, options.ToArray()), (folderInfo));
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
        public static void emptyAction(string text) {}
    }
}
