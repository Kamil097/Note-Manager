﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace thoughtsApp.Tui
{
    public class FileViewer
    {
        private enum mode { Update, Delete }

        private List<(string name,string id)> NoteInformation;
        private int NoteIndex;

        private List<mode> ModeList;
        private int ModeIndex;

        private bool WasNoteChanged;
       
        public FileViewer(List<(string name, string id)> noteInformation, int noteIndex)
        {
            NoteInformation = noteInformation;
            NoteIndex = noteIndex;
            ModeList = new List<mode> {mode.Update, mode.Delete };
            ModeIndex = 0;
            WasNoteChanged = true;
        }
        private async Task<string> DownloadText() 
        {
            var text = FileManager.GetFileText(NoteInformation[NoteIndex].id);
            while (!text.IsCompleted)
                Visuals.WaitingAnimation("Downloading note text");
            return text.Result;
        }
        public static void dupa(string text)
        {
            WriteLine();
        }
        private async Task ModeSwitch(string fileText) 
        {
            Clear();
            switch (ModeList[ModeIndex])
            {
                case mode.Update:
                    WriteLine("Welcome to note editor, insert text you want to append to your note (sadly you can't edit it completely xd)");
                    string text = ReadLine();
                    var updating = FileManager.UpdateNoteToGoogleDrive(NoteInformation[NoteIndex].id,fileText +" " + text);
                    while (!updating.IsCompleted)
                        Visuals.WaitingAnimation("Updating note");
                    break;
                case mode.Delete:
                    var deleting = FileManager.DeleteNoteFromGoogleDrive(NoteInformation[NoteIndex].id);
                    while (!deleting.IsCompleted)
                        Visuals.WaitingAnimation("Deleting note");
                    break;
            }        
        }
        public async Task Run()
        {
            var noteText = "";
            ConsoleKey keyPressed;
            do
            {
                Clear();

                if(WasNoteChanged)
                    noteText = await DownloadText();

                WriteLine($"{NoteInformation[NoteIndex].name}\n{noteText}\nSelected mode: {ModeList[ModeIndex]}");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.LeftArrow)
                {
                    if (NoteIndex == 0)
                        NoteIndex = NoteInformation.Count() - 1;
                    else
                        NoteIndex--;
                    WasNoteChanged = true;
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    if (NoteIndex == NoteInformation.Count() - 1)
                        NoteIndex = 0;
                    else
                        NoteIndex++;
                    WasNoteChanged = true;
                }
                else if (keyPressed == ConsoleKey.UpArrow)
                {
                    if (ModeIndex == ModeList.Count() - 1)
                        ModeIndex = 0;
                    else
                        ModeIndex++;
                    WasNoteChanged = false;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    if (ModeIndex == ModeList.Count() - 1)
                        ModeIndex = 0;
                    else
                        ModeIndex++;
                    WasNoteChanged = false;
                }
                else if (keyPressed == ConsoleKey.Enter)
                {
                    ModeSwitch(noteText);
                    break;
                }
            } while (keyPressed != ConsoleKey.Escape);
        }
    }
}