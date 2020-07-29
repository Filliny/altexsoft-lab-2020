using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu(args);

            if (args.Length == 0)
            {
                Console.WriteLine("You didn't give command line options! ");
                menu.GetArgs();
            }

            menu.Process();

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
        }


    }


    class Menu
    {
        private string[] argums;


        public Menu(string[] argums)
        {
            this.argums = argums;
        }

        public void GetArgs()
        {
            Array.Resize(ref argums, 1);
            Console.Write("Give filename or path to process: ");

            //Can skip this cos we parse in Process method
            while (!ArgValidator.PathTryParse(Console.ReadLine(), ref argums[0]))
            {
                Console.Write("Wrong path! Give right value: ");
            }

            if ((new FileInfo(argums[0]).Attributes & FileAttributes.Directory) != FileAttributes.Directory)
            {
                Array.Resize(ref argums, 2);
                Console.Write("Give file process option (remove, showten or thirdsentence): ");
                argums[1] = Console.ReadLine().ToLower();

                if (argums[1].Equals("remove"))
                {
                    Array.Resize(ref argums, 3);
                    Console.Write("Give word or char to remove : ");
                    argums[2] = Console.ReadLine().ToLower();
                }
            }
        }

        public void Process()
        {

            if (ArgValidator.PathTryParse(argums[0], ref argums[0]))
            {


                if (argums.Length == 1)
                {
                    if (argums[0].Equals("-h"))
                    {
                        Console.WriteLine("help");
                        return;
                    }

                    //ignore file if only path given
                    var dir = Path.GetDirectoryName(argums[0]);
                }
                else
                {
                    FileClass file = new FileClass(argums[0]);

                    switch (argums[1])
                    {
                        case "remove":
                            file.MakeBackup();
                            file.Remove(argums[2]);
                            break;

                        default:
                            Console.WriteLine("Wrong arguments given! type -h for help display");
                            break;
                    }
                }
            }
            else if (Path.GetFileName(argums[0]).ToLower().Equals("-h"))
            {
                ShowHelp();
            }
            else
            {
                Console.WriteLine("Wrong path!");
            }


        }



        public void ShowHelp()
        {
            Console.WriteLine("HELP");
        }

    }

    static class ArgValidator
    {
        private static List<string> actions = new List<string> { "remove", "showten", "thirdsentence" };


        public static bool Validate(ref string[] arguments)
        {
            if (PathTryParse(arguments[0], ref arguments[0]))
            {
                if (((new FileInfo(arguments[0]).Attributes & FileAttributes.Directory) != FileAttributes.Directory))
                {
                    return true;
                }
                else if (arguments.Length >=2)
                {
                    if (ActionsVal(arguments[1]))
                    {
                        if (arguments[1] == "remove" && arguments.Length == 3)
                        {
                            return true;
                        }
                        else if(arguments.Length ==2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File or folder path wrong! ");
            }

            return false;
        }

        public static bool ActionsVal(string actionCandidate)
        {
            return actions.Contains(actionCandidate);
        }



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
}

