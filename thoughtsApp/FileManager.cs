using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class FileManager
	{
		public static void CreateNote(string description)
		{
			File.AppendAllText(FileConfig.FullPath, description);
		}
		public static void Initialize()
		{
			if (!Directory.Exists(FileConfig.FullPath))
			{
				Directory.CreateDirectory(FileConfig.FullPath);
			}

		}
	}
}
