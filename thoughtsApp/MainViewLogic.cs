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
                return (0, false);

            if (!Verifiers.optionWerifier(optionsCount, text))
                return (0, true);

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

            MainView.FileViewer(list, option - 1, folderId);
            return true;
        }
        //For loop to continue, we have to give TRUE
        public static (bool loop, int note) FileViewerLoop(int note, int limit)
        {

            string text = Console.ReadLine();
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
            return (!Verifiers.ExitConditions(text), note);
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
        public static Dictionary<string, List<string>> GetExpressionSentences(string expression)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
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
                    dict.Add(note["name"].Value<string>(), exprSentences);
            }
            return dict;
        }


    }
}
