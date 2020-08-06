using System;
using System.IO;
using System.Text.RegularExpressions;
using ConsoleApp.FileHelpers;
using ConsoleApp.InputWorkers;

namespace ConsoleApp.FileProcess
{
    internal interface IRemover
    {
        public void RemoveWord(MyArgs args);
        string ReadFile(string path);
    }

    class Remover : Counter, IRemover
    {
        public void RemoveWord(MyArgs args)
        {
            string workPath = args.FilePath;
            string text = ReadFile(args.FilePath);

            string savePath = workPath + ".bak"; //save backup
            File.Copy(args.FilePath, savePath,true);

            string regexStr = @"\b(" + args.Word.ToLower() + @")\b";

            if (Regex.Matches(text, regexStr).Count == 0)
            {
                Console.WriteLine("Searched phrase not found!");
                return;
            }

            text = Regex.Replace(text, regexStr, "", RegexOptions.IgnoreCase);

            Console.WriteLine(text);

            File.WriteAllText(workPath, text);
        }
    }
}