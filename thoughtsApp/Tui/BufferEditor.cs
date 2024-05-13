using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace thoughtsApp.Tui
{
    public class BufferEditor
    {
        public int left { get; set; }
        public int top { get; set; }
        public string input { get; set; }
        public string header { get; set; }  
        public BufferEditor(string inputText, string header)
        {
            this.input = inputText;
            this.header = header;
        }

        public string Run()
        {
            var maxheight = WindowHeight;
            SetCursorPosition(0, CursorTop);
            StringBuilder editedText = new StringBuilder(input.Trim());
            ConsoleKeyInfo key;
            WriteLine(header);
            Write(editedText);

            do
            {
                left = GetCursorPosition().Left;
                top = GetCursorPosition().Top;
                (int maxRow,int maxCol) maxIndex = GetMaxIndex(editedText.ToString());
                SetCursorPosition(left, top);
                key = ReadKey();

                if (key.Key == ConsoleKey.Backspace && editedText.Length > 0)
                {
                    editedText.Remove(left + (top-1) * WindowWidth - 1, 1);
                    Clear();
                    WriteLine(header);
                    SetCursorPosition(0, CursorTop);
                    Write(editedText.ToString());
                    if (left == 0)
                        SetCursorPosition(WindowWidth - 1, top - 1);
                    else
                        SetCursorPosition(left - 1, top);

                }
                else if (key.Key == ConsoleKey.RightArrow && (top < maxIndex.maxRow || maxIndex.maxCol > left - 1))
                {
                    if (WindowWidth - 1 == left)
                        SetCursorPosition(0, top + 1);
                    else
                        SetCursorPosition(left + 1, top);
                }
                else if (key.Key == ConsoleKey.LeftArrow && left > 0)
                {
                    SetCursorPosition(left - 1, top);
                }
                else if (key.Key == ConsoleKey.LeftArrow && top > 1)
                {
                    SetCursorPosition(WindowWidth - 1, top - 1);
                }
                else if (key.Key == ConsoleKey.DownArrow && top < maxIndex.maxRow)
                {
                    if (top != maxIndex.maxRow - 1 || left <= maxIndex.maxCol + 1)
                        SetCursorPosition(left, top + 1);
                }
                else if (key.Key == ConsoleKey.UpArrow && top > 1)
                {
                    SetCursorPosition(left, top - 1);
                }
                else if (key.Key == ConsoleKey.Escape)
                    return "";
                else if (!char.IsControl(key.KeyChar))
                {

                    editedText.Insert(left + (top-1) * WindowWidth, key.KeyChar);

                    if (left == WindowWidth - 1)
                        SetCursorPosition(0, top + 1);

                    if (left + (top-1) * WindowWidth < editedText.Length - 1)
                    {
                        Clear();
                        WriteLine(header);
                        SetCursorPosition(0, CursorTop);
                        Write(editedText.ToString());
                        if (WindowWidth - 1 == left)
                            SetCursorPosition(0, top + 1);
                        else
                            SetCursorPosition(left + 1, top);
                    }
                }
                
                
            }
            while (key.Key != ConsoleKey.Enter);
            return editedText.ToString();   
        }
        private static (int maxRow, int maxCol) GetMaxIndex(string editText)
        {
            (int maxRow, int maxCol) result = (0, 0);
            int editTextlen = editText.Length;
            while (editTextlen > WindowWidth)
            {
                editTextlen -= WindowWidth;
                result.maxRow++;
            }
            if(editTextlen > 0)
                result.maxRow++;
            result.maxCol = editTextlen-1;
            return result;
        }
    }
}
