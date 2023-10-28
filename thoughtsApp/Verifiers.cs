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
			if (option.Length != 1)
				return false;

			if (!int.TryParse(option, out int number))
				return false;

			if (number > optionsLimit || number < 1)
				return false;

			return true;
		}
	}
}
