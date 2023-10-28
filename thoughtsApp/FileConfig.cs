using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
    public static class FileConfig
    {
        public static string UserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string FullPath = Path.Combine(UserPath, "Desktop\\Thoughts");
		public static string folderId = "12Cy_QIkTUjqD-Gqg7jHCa2RERpMGj3m0";
		public static string credentialsPath = "credentials.json";
	}
}
