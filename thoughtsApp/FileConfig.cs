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
        public static readonly string folderId = "12Cy_QIkTUjqD-Gqg7jHCa2RERpMGj3m0";
        public static readonly string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string combinedNotes = Path.Combine(projectDirectory, @"FileFolder\combinedNotes.json");
        public static readonly string credentialsPath = Path.Combine(projectDirectory, @"FileFolder\credentials.json");
        public static readonly string encryptionKey = "!@#whoami#@!";
    }
}
