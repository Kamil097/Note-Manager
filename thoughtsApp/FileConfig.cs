using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
    public static class FileConfig
    {
        public static readonly string UserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static readonly string FullPath = Path.Combine(UserPath, "Desktop\\Thoughts");
        public static readonly string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string credentialsPath = Path.Combine(projectDirectory, @"FileFolder\credentials.json");
        public static readonly string encryptionKey = "!@#whoami#@!";
        public static readonly string thoughtsFolder = "12Cy_QIkTUjqD-Gqg7jHCa2RERpMGj3m0";
        public static readonly string rulesFolder = "1JINSXeqerMT-yjPGhRJBJ_QFFRy9DlI2";
        public static readonly string journalFolder = "1z4kHf255o0KWCRAF8RptnKAGSIQ3EH9o";
        public static readonly string mainFolder = "1y00Xd9fVkm7GeDF5gYspTKRP1nER9ldG";
        public static readonly string combinedNotes = Path.Combine(projectDirectory, @"FileFolder");
        // editable variables
        public static string folderId = null;
        public static string folderName = null;
    }
}
