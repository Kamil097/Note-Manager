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
        public static async Task FolderChoiceMenu()
        {
            var folderInfo = new List<(string name, string id)>();
            folderInfo = await FileManager.getCurrentFolders();
            List<(string option, object property)> options = new List<(string option, object property)>();
            for (int i = 0; i < folderInfo.Count; i++)
            {
                options.Add(($"{i + 1}.", $"{folderInfo[i].name}"));
            }
            Menu menu = new Menu("Chooser folder", options.ToArray());
            int option = menu.Run();
            //update configuration
            FileConfig.folderId = folderInfo[option].id;
            FileConfig.folderName = folderInfo[option].name;
        }
        public static bool DoesTxtFileExist(string path, out string result) 
        {
            string text = "";
            Console.Clear();
            try
            {
                if (File.Exists(path))
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
    }
}
