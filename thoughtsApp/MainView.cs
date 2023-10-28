using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class MainView
	{
		public static int mainWindowOptionsLength = 3;
		public static int MainWindowOptionListLoop()
		{
			int option = 0;
			do
			{
				Console.Clear();
				Console.WriteLine("1. Upload your thought.");
				Console.WriteLine("2. View thoughts");
				Console.WriteLine("3. Exit program");
				option = MainViewLogic.MainWindowLoopCondition();
			}
			while (option==0);
			
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
	}
}
