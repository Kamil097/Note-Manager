using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace thoughtsApp
{
    public static class MainViewLogic
    {
        //Loop that handles sending new thoughts
        public static async Task<bool> ThoughtLoopCondition()
        {
            try
            {
                string text = Console.ReadLine();

                if (Verifiers.ExitConditions(text))
                    return false;

                await FileManager.SendNewNote(text, FileConfig.folderId);
                await Task.Delay(1000);
            }
            finally
            {
                Console.Clear();
            }
            return true;
        }
        //Loop that handles main window options
        public static (int option, bool loop) OptionsLoopCondition(int optionsCount)
        {

            string text = Console.ReadLine();
            if (Verifiers.ExitConditions(text))
                return (0, true);

            if (!Verifiers.optionWerifier(optionsCount, text))
                return (0, false);

            int.TryParse(text, out int option);
            return (option, false);
        }
        //Loop that handles explorer
        public static bool ThoughtListLoop(string folderId, List<(string name, string id)> list)
        {
            string text = Console.ReadLine();
            if (Verifiers.ExitConditions(text))
                return false;

            if (!Verifiers.optionWerifier(list.Count, text))
                return true;

            int.TryParse(text, out int option);

            MainView.FileViewer(list, option - 1);
            return true;
        }
        //For loop to continue, we have to give TRUE
        public static  (bool loop, int note, bool edit) FileViewerLoop(int note, int limit,bool doEdit, (string text, string id) fileinfo)
        {

            bool edit = false;
            string text = Console.ReadLine();

            if (doEdit)
            {
                 var dupa = FileManager.UpdateNoteToGoogleDrive(fileinfo.id, fileinfo.text + text);
                 while (!dupa.IsCompleted) { } // im a moron, but it works
            }

            if (text.Equals("prev", StringComparison.OrdinalIgnoreCase))
            {
                if (note > 1)
                {
                    note -= 1;
                }
            }
            else if (text.Equals("next", StringComparison.OrdinalIgnoreCase))
            {
                if (note < limit)
                {
                    note += 1;
                }
            }
            else if (text.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                edit = true;  
            }
            return (!Verifiers.ExitConditions(text), note,edit);
        }
        public static (bool loop, int note) RandomThoughtLoop(int note, int limit)
        {
            string text = Console.ReadLine();
            if (text.Equals("next", StringComparison.OrdinalIgnoreCase))
            {
                note = new Random().Next(0, limit);
            }

            return (!Verifiers.ExitConditions(text), note);
        }
        //Key is note name, and list represents sentences of such note with given expression.
        public static List<(string name, List<string> sentence)> GetExpressionSentences(string expression)
        {
            List<(string name, List<string> sentence)> list = new List<(string name, List<string> sentence)>();
            string line;
            string noteName = "";
            JArray array = (JArray)FileManager.GetJsonObject(FileConfig.combinedNotes)["data"];
            foreach (JObject note in array)
            {
                var sentences = note["text"].Value<string>().Split('.').ToList();
                List<string> exprSentences = new List<string>();
                foreach (var sentence in sentences)
                {
                    if (sentence.Contains(expression))
                    {
                        exprSentences.Add(sentence);
                    }
                }
                if (exprSentences.Count > 0)
                    list.Add((note["name"].Value<string>(), exprSentences));
            }
            return list;
        }


    }
}
