using ConsoleApp.InputWorkers;
using System;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;

namespace ConsoleApp.FileProcess
{
    static class Counter
    {
        public static void CountWords(MyArgs args)
        {
            string Text = FileReader.ReadFile(args.FilePath);

            string regExpr = @"\b\w+[-']*\w*\b";
            var result = Regex.Matches(Text, regExpr);
            Console.WriteLine($"\nThere is {result.Count} words in given text.");
            Console.WriteLine("Every ten word is: ");

            for (int i = 9; i < result.Count;)
            {
                Console.Write(result[i]);
                i += 10;

                if (i < result.Count)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.Write(".\n");
                }
            }
        }
    }
}
