using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using WindowsInput;
using WindowsInput.Native;
using System.Xml.Serialization;
using System.Drawing;

namespace thoughtsApp.Tui
{
    public class BufferEditor
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public string Input { get; set; }
        public string Header { get; set; }
        public ColorEnum CurrentColor { get; set; } = ColorEnum.Default;
        public enum ColorEnum {Default=1, Red=2, Yellow=3, Blue=4, Purple=5, Green=6}
        public bool IsFormattingActive { get; set; } = false;
        public int EnumLength => Enum.GetValues(typeof(ColorEnum)).Length;  
        public BufferEditor(string inputText, string header)
        {
            this.Input = inputText;
            this.Header = header;
        }

        public string Run()
        {
            var maxheight = WindowHeight;
            SetCursorPosition(0, CursorTop);
            StringBuilder editedText = new StringBuilder(Input.Trim());
            ConsoleKeyInfo key;
            WriteLine(Header);
            Write(editedText);

            do
            {
                Left = GetCursorPosition().Left;
                Top = GetCursorPosition().Top;
                (int maxRow, int maxCol) maxIndex = GetMaxIndex(editedText.ToString());
                SetCursorPosition(Left, Top);
                key = ReadKey();

                if (key.Key == ConsoleKey.Backspace && editedText.Length > 0)
                {
                    editedText.Remove(Left + (Top - 1) * WindowWidth - 1, 1);
                    Clear();
                    WriteLine(Header);
                    SetCursorPosition(0, CursorTop);
                    Write(editedText.ToString());
                    if (Left == 0)
                        SetCursorPosition(WindowWidth - 1, Top - 1);
                    else
                        SetCursorPosition(Left - 1, Top);

                }
                else if (key.Key == ConsoleKey.RightArrow && (Top < maxIndex.maxRow || maxIndex.maxCol > Left - 1))
                {
                    if (WindowWidth - 1 == Left)
                        SetCursorPosition(0, Top + 1);
                    else
                        SetCursorPosition(Left + 1, Top);
                }
                else if (key.Key == ConsoleKey.LeftArrow && Left > 0)
                {
                    SetCursorPosition(Left - 1, Top);
                }
                else if (key.Key == ConsoleKey.LeftArrow && Top > 1)
                {
                    SetCursorPosition(WindowWidth - 1, Top - 1);
                }
                else if (key.Key == ConsoleKey.DownArrow && Top < maxIndex.maxRow)
                {
                    if (Top != maxIndex.maxRow - 1 || Left <= maxIndex.maxCol + 1)
                        SetCursorPosition(Left, Top + 1);
                }
                else if (key.Key == ConsoleKey.UpArrow && Top > 1)
                {
                    SetCursorPosition(Left, Top - 1);
                }
                else if (key.Key == ConsoleKey.Escape)
                    return "";
                else if(key.Modifiers.HasFlag(ConsoleModifiers.Alt) && key.Key == ConsoleKey.V)
                {
                    CurrentColor = GetNextColor();
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    UpdateColor();
                    editedText.Insert(Left + (Top - 1) * WindowWidth, key.KeyChar);

                    if (Left == WindowWidth - 1)
                        SetCursorPosition(0, Top + 1);

                    if (Left + (Top - 1) * WindowWidth < editedText.Length - 1)
                    {
                        Clear();
                        WriteLine(Header);
                        SetCursorPosition(0, CursorTop);
                        Write(editedText.ToString());
                        if (WindowWidth - 1 == Left)
                            SetCursorPosition(0, Top + 1);
                        else
                            SetCursorPosition(Left + 1, Top);
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
            if (editTextlen > 0)
                result.maxRow++;
            result.maxCol = editTextlen - 1;
            return result;
        }
        public ColorEnum GetNextColor() {
            var index = (int)CurrentColor;
            int nextValue = index == EnumLength ? 1 : index + 1;
            IsFormattingActive = true;
            return (ColorEnum)nextValue;
        }
        public void UpdateColor() {
            switch (CurrentColor) {
                case ColorEnum.Default:
                    ForegroundColor = ConsoleColor.White;
                    break;
                case ColorEnum.Red:
                    ForegroundColor = ConsoleColor.Red;  
                    break;
                case ColorEnum.Yellow:
                    ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ColorEnum.Blue:
                    ForegroundColor = ConsoleColor.Blue;
                    break;
                case ColorEnum.Purple:
                    ForegroundColor = ConsoleColor.Magenta;
                    break;
                case ColorEnum.Green:
                    ForegroundColor = ConsoleColor.Green;
                    break;
            }
        }
    }
}
