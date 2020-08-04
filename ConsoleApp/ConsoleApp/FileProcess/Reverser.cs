using System;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;
using ConsoleApp.InputWorkers;


namespace ConsoleApp.FileProcess
{
    class Reverser : FileReader
    {
        public void ReverseSentence(MyArgs args, int sentenceNum)
        {
            string text = ReadFile(args.FilePath);

            string regExpr = @"[A-Za-z](.*?|\n?|\r?)*?[.?!]+(?=\W)";
            string wordExpr = @"\b\w+[-']*\w*\b";
            var sentences = Regex.Matches(text, regExpr);


            string sentence = sentences[--sentenceNum].Value;

            MatchCollection words = Regex.Matches(sentence, wordExpr);

            Console.WriteLine($"\nReversed sentence number {++sentenceNum} :\n");

            foreach (Match word in words)
            {
                Console.Write(new String(word.Value.ToCharArray().Reverse().ToArray()));
                Console.Write(" ");
            }

            Console.Write(".");
        }
    }
}