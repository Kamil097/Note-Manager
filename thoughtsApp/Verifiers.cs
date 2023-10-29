using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp
{
	public static class Verifiers
	{
		public static bool optionWerifier(int optionsLimit, string option) 
		{
			if (int.TryParse(option, out int number) && number <= optionsLimit && number > 0)
				return true;

			return false;

		}
		public static bool ExitConditions(string text)
		{
			if (text.Equals("xd", StringComparison.OrdinalIgnoreCase))
				Environment.Exit(0);

			if (text.Equals("x", StringComparison.OrdinalIgnoreCase))
				return true;

			return false;
		}
	}
}
