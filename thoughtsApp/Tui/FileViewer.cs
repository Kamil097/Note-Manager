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

        private string Phrase;
       
        public FileViewer(List<(string name, string id)> noteInformation, int noteIndex, string phrase)
        {
            NoteInformation = noteInformation;
            NoteIndex = noteIndex;
            ModeList = new List<mode> {mode.Update, mode.Delete };
            ModeIndex = 0;
            WasNoteChanged = true;
            Phrase = phrase;    
        }
        private void DisplayText(string noteText) 
        {
            if (string.IsNullOrEmpty(Phrase))
                WriteLine($"{NoteInformation[NoteIndex].name.Replace(".txt","")}\n{noteText}\nSelected mode: {ModeList[ModeIndex]}");
            else
            {
                var sentences = noteText.Split('.');
                foreach (var sentence in sentences)
                {
                    if (!string.IsNullOrEmpty(sentence))
                        MenuLogic.ReadSentenceWithColoredExpression($"{sentence}.",Phrase);
                }
                WriteLine($"\nSelected mode: { ModeList[ModeIndex]}");
            }
        }
        private async Task<string> DownloadText() 
        {
            var text = FileManager.GetFileText(NoteInformation[NoteIndex].id);
            while (!text.IsCompleted)
                Visuals.WaitingAnimation("Downloading note text");
            return text.Result;
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

                DisplayText(noteText);

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
        private async Task ModeSwitch(string fileText)
        {
            Clear();
            switch (ModeList[ModeIndex])
            {
                case mode.Update:
                    BufferEditor buff = new BufferEditor(fileText,"Update your note.");
                    var edited = buff.Run();
                    string noteTitle = new BufferEditor(NoteInformation[NoteIndex].name, "Note title:").Run().Trim();
                    var updating = FileManager.UpdateNoteToGoogleDrive(noteTitle,NoteInformation[NoteIndex].id, edited);
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
    }
}
