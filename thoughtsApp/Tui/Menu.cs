using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Drive.v3.ChannelsResource;
using static System.Console;

namespace thoughtsApp.Tui
{
    public class Menu
    {
        private int SelectedIndex;
        private (string functionArgument, string option)[] Infos;
        private string Prompt;
        private Action<string> DelegateFunction;
      

        public Menu(string prompt,  Action<string> function, (string functionArgument, string option)[] infos)
        {
            Prompt = prompt;
            Infos = infos;
            SelectedIndex = 0;
            DelegateFunction = function;    
        }
        private void DisplayOptions()
        {
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
            WriteLine(Prompt);
            for (int i = 0; i < Infos.Length; i++)
            {
                string currentOption = $" {Infos[i].option}";
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
                WriteLine($"{prefix} << {currentOption.Replace(".txt","")} >>");
                ResetColor();
                DelegateFunction($"{Infos[i].functionArgument}"); //run delegate
            }
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
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    if (SelectedIndex == 0)
                        SelectedIndex = Infos.Count() - 1;
                    else
                        SelectedIndex--;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    if (SelectedIndex == Infos.Count() - 1)
                        SelectedIndex = 0;
                    else
                        SelectedIndex++;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
}
