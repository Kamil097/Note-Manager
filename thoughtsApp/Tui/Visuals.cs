using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thoughtsApp.Tui
{
    public class Visuals
    {
        public static void WaitingAnimation(string text)
        {
            Console.Clear();
            for (int i = 1; i < 3; i++)
            {
                Console.Write(text.ToUpper());
                for (int j = 0; j <= i; j++)
                {
                    Console.Write(".");
                    Thread.Sleep(500);
                }
                Console.Clear();
            };
        }

    }
}
