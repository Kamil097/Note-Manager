﻿using System;
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
		public static readonly string workingDirectory = Environment.CurrentDirectory;
		public static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
		public static readonly string mergedTextPath = Path.Combine(projectDirectory, @"FileFolder/mergedText.txt");
		public static readonly string credentialsPath = Path.Combine(projectDirectory, @"ConfigFiles/credentials.json");
		public static readonly string noteCode = "%@#$2#4@2#31%25@";
	}
}
