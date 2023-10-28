using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class MainViewLogic
	{
		public static bool ThoughtLoopCondition()
		{
			string text = Console.ReadLine();

			Verifiers.ExitConditions(text);

			FileManager.SendNewNote(text,FileConfig.folderId);
			Console.Clear();
			return true;
		}
		public static int MainWindowLoopCondition(int optionsCount)
		{
			
			string text = Console.ReadLine();

			if (!Verifiers.optionWerifier(optionsCount, text))
				return 0;
			else
			{
				int.TryParse(text, out int option);
				return option;
			}
			
		}
		public static bool ThoughtListLoop(string folderId, List<(string name, string id)> list) 
		{
			string text = Console.ReadLine();
			if (!Verifiers.ExitConditions(text))
			{
				return false;
			}

			if (!Verifiers.optionWerifier(list.Count, text))
				return false;
			else
			{
				int.TryParse(text, out int option);

				MainView.FileViewer(list[option - 1], folderId);
				return true;
			}
			
		}
	}
}
