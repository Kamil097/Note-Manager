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
    }
}
