using System;
using System.IO;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;
using ConsoleApp.InputWorkers;

namespace ConsoleApp.FileProcess
{
    static class Remover
    {
        public static void Remove(MyArgs args)
        {
            string WorkPath = args.FilePath;
            string Text = FileReader.ReadFile(args.FilePath);

            string savePath = WorkPath + ".bak"; //save backup
            File.WriteAllText(savePath, Text);

            string regexStr = @"\b(" + args.Word.ToLower() + @")\b";

            if (Regex.Matches(Text, regexStr).Count == 0)
            {
                Console.WriteLine("Searched phrase not found!");
                return;
            }

            Text = Regex.Replace(Text, regexStr, "", RegexOptions.IgnoreCase);

            Console.WriteLine(Text);

            File.WriteAllText(WorkPath, Text);
        }
    }
}