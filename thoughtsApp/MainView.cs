using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class MainView
	{
		public static int MainWindowOptionListLoop()
		{
			int option = 0;
			do
			{
				Console.Clear();
				Console.WriteLine("1. Upload your thought.");
				Console.WriteLine("2. View thoughts");
				Console.WriteLine("3. Exit program");
				option = MainViewLogic.MainWindowLoopCondition(3);
			}
			while (option == 0);

			return option;
		}
		public static void ThoughtLoop()
		{
			do
			{
				Console.Clear();
				Console.WriteLine("Welcome to thought uploader!\n");
				Console.WriteLine("----------------------------");
				Console.WriteLine("1. Type X to go back to menu.");
				Console.WriteLine("2. Type XD to leave program");
				Console.WriteLine("----------------------------\n");
				Console.WriteLine("Share your thought below: \n\n\n");
			}
			while (MainViewLogic.ThoughtLoopCondition());
		}
		public static void DriveExplorer(string folderId)
		{
			var list = FileManager.GetNotesInfoFromDrive(folderId);
			var length = list.Count();
			do
			{
				Console.Clear();
				Console.WriteLine("List of current notes.");
				Console.WriteLine("----------------------");
				for(int i=0; i<length; i++)
				{
					Console.WriteLine($"{i+1} {list[i].name}");
				}
			}
			while (MainViewLogic.ThoughtListLoop(folderId, list));

			
		}
		public static void FileViewer((string name, string id) fileInfo,string folderId)
		{
			var text = FileManager.GetFileText(fileInfo.id);
			string response = "";

			do
			{
				Console.Clear();
				Console.WriteLine($"Notatka: {fileInfo.name}");
				Console.WriteLine(text);
				response = Console.ReadLine();
			}
			while (!Verifiers.ExitConditions(response));
		}
	}
}
