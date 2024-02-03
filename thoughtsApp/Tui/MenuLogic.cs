using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static async Task<(Menu menu, List<(string Name, string Id)> infos)> PrepareFolderListMenu()
        {
            var folderInfo = await FileManager.getCurrentFolders();
            List<(string option, object property)> options = new List<(string option, object property)>();
            for (int i = 0; i < folderInfo.Count; i++)
            {
                options.Add(($"{i + 1}.", $"{folderInfo[i].name}"));
            }
            options.Add(("Go", "back"));
            return (new Menu("Choose folder", options.ToArray()), (folderInfo));
        }

        public static async Task<(Menu menu, List<(string Name, string Id)> infos)> GetMenuAndNotesInfo()
        {
            var notesInfo = await FileManager.GetNotesInfoFromDrive();
            List<(string option, object property)> options = new List<(string option, object property)>();

            for (int i = 0; i < notesInfo.Count; i++)
            {
                options.Add(($"{i + 1}.", $"{notesInfo[i].name}"));
            }
            options.Add(("Go", "back"));
            return (new Menu("List of current notes:",options.ToArray()), (notesInfo));     
        } 
        public static void FileViewer(List<(string name, string id)> list, int note)
        {
            (bool loop, int note, bool edit) continuation = (true, note, false);
            do
            {
                Console.Clear();
                if (continuation.edit)
                    Console.WriteLine("Add text to your current note.");
                else
                    Console.WriteLine("| X- Go Back | XD - Leave Program |\n| Next - Next Note | Prev - Previous Note |\n| Update - Update Note | Delete - Delete Note |\n");

                var text = FileManager.GetFileText(list[continuation.note].id);
                FilesLookup(list[continuation.note], text);

                //continuation = MainViewLogic.FileViewerLoop(continuation.note, list.Count - 1, continuation.edit, (text, list[continuation.note].id));
            }
            while (continuation.loop);
        }
        public static void FilesLookup((string name, string id) fileInfo, string text)
        {
            Console.WriteLine($"Notatka: {fileInfo.name}\n");
            Console.WriteLine(text);
        }
    }
}
