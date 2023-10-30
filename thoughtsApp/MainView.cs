using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace thoughtsApp
{
	public static class MainView
	{
		public delegate bool LoopDelegate();
		public static int MainWindowOptionListLoop()
		{
			int option = 0;
			do
			{
				Console.Clear();
				Console.WriteLine("1. Upload your thought.");
				Console.WriteLine("2. View thoughts explorer.");
				Console.WriteLine("3. View random thought.");
				Console.WriteLine("4. View everything.");
				option = MainViewLogic.MainWindowLoopCondition(4);
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
			while (MainViewLogic.ThoughtLoopCondition().Result);
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
		public static void FilesLookup((string name, string id) fileInfo, string text)
		{
			Console.WriteLine($"Notatka: {fileInfo.name}");
			Console.WriteLine(text);
		}
		public static void RandomFileViewer(string folderId)
		{
			var list = FileManager.GetNotesInfoFromDrive(folderId);
			int note = new Random().Next(0, list.Count-1);
			(bool loop, int note) continuation = (true, note);
			do
			{
				Console.Clear();
				Console.WriteLine("| X- Go Back | XD - Leave Program |\n| Next - Next Note |\n");
				var text = FileManager.GetFileText(list[continuation.note].id);
				FilesLookup(list[continuation.note], text);
				continuation = MainViewLogic.RandomThoughtLoop(continuation.note, list.Count-1);
			}
			while (continuation.loop);
		}
		public static void FileViewer(List<(string name, string id)> list,int note, string folderId)
		{
			(bool loop, int note) continuation = (true, note);
			do
			{
				Console.Clear();
				Console.WriteLine("| X- Go Back | XD - Leave Program |\n| Next - Next Note | Prev - Previous Note |\n");
				var text = FileManager.GetFileText(list[continuation.note].id);
				FilesLookup(list[continuation.note], text);
				continuation = MainViewLogic.FileViewerLoop(continuation.note, list.Count-1);
			}
			while (continuation.loop);
		}
		
	}
}
