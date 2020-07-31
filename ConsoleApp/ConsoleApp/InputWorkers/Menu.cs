using ConsoleApp.Instant;
using System;
using System.IO;
using ConsoleApp.FileProcess;


namespace ConsoleApp.Statics
{
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
                argums[0] = "-D"; //if path is folder stop collecting arguments
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

       
    }

}
