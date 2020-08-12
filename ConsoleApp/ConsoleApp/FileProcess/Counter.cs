using ConsoleApp.InputWorkers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;

namespace ConsoleApp.FileProcess
{
    internal interface ICounter
    {
        public void CountWords(MyArgs args);
       
    }

    class Counter : FileReader, ICounter
    {
        public void CountWords(MyArgs args)
        {
            string text = ReadFile(args.FilePath);

            string regExpr = @"\b\w+[-']*\w*\b";
            var result = Regex.Matches(text, regExpr);
            Console.WriteLine($"\nThere is {result.Count} words in given text.");
            Console.WriteLine("Every ten word is: ");

            var everyTen = result.Where((item, index) => (index - 9) % 10 == 0)
                .Select(n => n.Value).ToArray();

            Console.Write(String.Join(',', everyTen));
            Console.Write(".\n");
        }
    }
}