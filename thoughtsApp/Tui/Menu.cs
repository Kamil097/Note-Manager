using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace thoughtsApp.Tui
{
    public class Menu
    {
        private int SelectedIndex;
        private (string option, object property)[] Options;
        private string Prompt;

        public Menu(string prompt, (string option, object property)[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }
        private void DisplayOptions()
        {
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = $" {Options[i].option} {Options[i].property}";
                string prefix;
                if (i == SelectedIndex)
                {
                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                WriteLine($"{prefix} << {currentOption}>>");
            }
            ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow && SelectedIndex > 0)
                {
                    SelectedIndex--;
                }
                else if (keyPressed == ConsoleKey.DownArrow && SelectedIndex < Options.Count() - 1)
                {
                    SelectedIndex++;
                }
            } while (keyPressed != ConsoleKey.Enter);
            //DisplayOptions();
            return SelectedIndex;
        }
    }
}
