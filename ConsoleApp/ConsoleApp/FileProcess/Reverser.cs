using System;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;
using ConsoleApp.InputWorkers;


namespace ConsoleApp.FileProcess
{
    internal interface IReverser
    {
        public void ReverseSentence(MyArgs args, int sentenceNum);
    }
    
    class Reverser : FileReader, IReverser
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

            string revdSentence = String.Join(" ", words.Select(x =>
                new String(x.Value.Reverse().ToArray())).ToArray()) + ".";

            Console.WriteLine(revdSentence);
        }
    }
}