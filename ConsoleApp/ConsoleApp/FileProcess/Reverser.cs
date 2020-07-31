using System;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;
using ConsoleApp.InputWorkers;


namespace ConsoleApp.FileProcess
{
    static class Reverser
    {
        public static void ReverseSentence (MyArgs args, int numSent)
        {
            
            string Text = FileReader.ReadFile(args.FilePath);

            string regExpr = @"[A-Za-z](.*?|\n?|\r?)*?[.?!]+(?=\W)";
            string wordExpr = @"\b\w+[-']*\w*\b";
            var sentences = Regex.Matches(Text, regExpr);

            string sentence = sentences[numSent].Value;

            MatchCollection words = Regex.Matches(sentence, wordExpr);

            Console.WriteLine($"\nReversed sentence number {++numSent} :\n");

            foreach (Match word in words)
            {
                Console.Write(new String(word.Value.ToCharArray().Reverse().ToArray()));
                Console.Write(" ");
            }

            Console.Write(".");
        }
    }
}