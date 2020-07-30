using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp.Instant
{
    class FileClass  //Reads/stores text file and stores methods for processing file
    {
        public string Text { get; private set; }
        public string WorkPath { get; private set; }

        public FileClass(string path)
        {
            WorkPath = path;
            Text = File.ReadAllText(WorkPath);
        }

        public void MakeBackup()
        {
            string savePath = WorkPath + ".bak";
            File.WriteAllText(savePath, Text);
        }

        public void Remove(string toRemove)
        {
            Text = File.ReadAllText(WorkPath);

            string regexStr = @"\b(" + toRemove.ToLower() + @")\b";

            if (Regex.Matches(Text, regexStr).Count == 0)
            {
                Console.WriteLine("Searched phrase not found!");
                return;
            }

            Text = Regex.Replace(Text, regexStr, "", RegexOptions.IgnoreCase);

            Console.WriteLine(Text);

            File.WriteAllText(WorkPath, Text);
        }

        public void CountWords()
        {
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

        public void ReverseSentence(int numSent)
        {
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
