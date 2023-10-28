using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class MainViewLogic
	{
		public static bool ThoughtLoop()
		{
			string text = Console.ReadLine();

			if (text.Equals("x",StringComparison.OrdinalIgnoreCase))
				return false;

			if(text.Equals("xd", StringComparison.OrdinalIgnoreCase))
				Environment.Exit(0);	

			FileManager.SendNewNote(text, FileConfig.credentialsPath, FileConfig.folderId);
			Console.Clear();
			return true;
		}
		public static int MainWindowLoop()
		{
			
			string text = Console.ReadLine();

			if (!Verifiers.optionWerifier(MainView.mainWindowOptionsLength, text))
				return 0;
			else
			{
				int.TryParse(text, out int option);
				return option;
			}
			
		}
	}
}
