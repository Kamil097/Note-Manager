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
        public BufferEditor(string inputText)
        {
            this.input = inputText;
        }

        public string Run()
        {
            SetCursorPosition(0, CursorTop);
            StringBuilder editedText = new StringBuilder(input.Trim());
            ConsoleKeyInfo key;
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
                    editedText.Remove(left + top * WindowWidth - 1, 1);
                    Clear();
                    SetCursorPosition(0, CursorTop);
                    Write(editedText.ToString());
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
                else if (key.Key == ConsoleKey.LeftArrow && top > 0)
                {
                    SetCursorPosition(WindowWidth - 1, top - 1);
                }
                else if (key.Key == ConsoleKey.DownArrow && top < maxIndex.maxRow)
                {
                    SetCursorPosition(left, top + 1);
                }
                else if (key.Key == ConsoleKey.UpArrow && top > 0)
                {
                    SetCursorPosition(left, top - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {

                    editedText.Insert(left + top * WindowWidth, key.KeyChar);
                    if (left + top * WindowWidth < editedText.Length - 1)
                    {
                        Clear();
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
            while (editTextlen > WindowWidth-1)
            {
                editTextlen -= WindowWidth - 1;
                result.maxRow++;
            }
            result.maxCol = editTextlen - 2;
            return result;
        }
    }
}
