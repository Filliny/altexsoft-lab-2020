using PowerArgs;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MyArgs parsed = new MyArgs();

            if (args.Length == 0)
            {
                Console.WriteLine("You didn't give command line options! ");
                args = Menu.GetArgs();
            }

            try
            {
                parsed = Args.Parse<MyArgs>(args);
            }
            catch (ArgException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<MyArgs>());
            }


            Menu.Process(new FileClass(parsed.FilePath), parsed);

            Console.ReadLine();
        }
    }

    class FileClass
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

            var words = Regex.Matches(sentence, wordExpr);

            Console.WriteLine($"\nReversed sentence number {++numSent} :\n");
            
            foreach (Match word in words)
            {
                Console.Write(new String(word.Value.ToCharArray().Reverse().ToArray()) );
                Console.Write(" ");
            }

            Console.Write(".");

        }
    }


    static class Menu
    {
        public static string[] GetArgs()
        {
            string[] argums = new string[2];

            Console.Write("Give filename or path to process: ");

            while (!PathValidator.PathTryParse(Console.ReadLine(), ref argums[1]))
            {
                Console.Write("Wrong path! Give right value: ");
            }


            if ((new FileInfo(argums[1]).Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                argums[0] = "-D";
            }
            else
            {
                Array.Resize(ref argums, 3);
                argums[0] = "-F";

                Console.Write("Give file process option :" +
                              "\n1 to remove " +
                              "\n2 for show each ten word" +
                              "\n3 to show each third sentence\n> ");

                while (argums[2] == null)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            argums[2] = "-R";
                            break;
                        case "2":
                            argums[2] = "-S";
                            break;
                        case "3":
                            argums[2] = "-T";
                            break;
                        default:
                            Console.WriteLine("Choose right number!");
                            break;
                    }
                }

                if (argums[2].Equals("-R"))
                {
                    Array.Resize(ref argums, 5);
                    argums[3] = "-W";
                    Console.Write("Give word or char to remove : ");
                    argums[4] = Console.ReadLine().ToLower();
                }
            }

            return argums;
        }

        public static void Process(FileClass file, MyArgs args)
        {
            if (args.FilePath != null)
            {
                if (args.Remove)
                {
                    file.MakeBackup();
                    file.Remove(args.Word);
                }

                if (args.ShowTen)
                {
                    file.CountWords();
                }

                if (args.ThirdSentense)
                {
                    file.ReverseSentence(2);
                }
            }

            if (args.DirPath != null)
            {
            }
        }
    }


    static class PathValidator //left from earlier validator to help with user input
    {
        public static bool PathTryParse(string input, ref string path)
        {
            if (!Path.IsPathRooted(input))
            {
                input = "\\" + input;
            }

            if (!Path.IsPathFullyQualified(input))
            {
                input = Environment.CurrentDirectory + input;
            }

            if (File.Exists(input) || Directory.Exists(input))
            {
                path = input;
                return true;
            }

            return false;
        }
    }


    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class MyArgs
    {
        [HelpHook, ArgShortcut("-h"), ArgDescription("Shows this help")]
        public bool Help { get; set; }

        [ArgExistingFile, ArgDescription("Path to file to process")]
        public string FilePath { get; set; }

        [ArgExistingDirectory, ArgDescription("Path to folder to browse")]
        public string DirPath { get; set; }

        [ArgShortcut("-R"), ArgDescription("Remove any word or char in all text. Use with -W")]
        public bool Remove { get; set; }

        [ArgShortcut("-W"), ArgDescription("Word to remove e.g. -W word_to_remove")]
        public string Word { get; set; }

        [ArgShortcut("-S"), ArgDescription("Show each ten word in text")]
        public bool ShowTen { get; set; }

        [ArgShortcut("-T"), ArgDescription("Show third reversed sentence in text")]
        public bool ThirdSentense { get; set; }
    }

    interface IFileProcessor
    {
    }
}

