using ConsoleApp.FileHelpers;
using System;
using System.IO;


namespace ConsoleApp.InputWorkers
{
    class Menu
    {
        public string[] GetArgs()
        {
            string[] argsManual = new string[2];

            Console.Write("Give filename or path to process: ");

            while (!PathValidator.PathTryParse(Console.ReadLine(), ref argsManual[1]))
            {
                Console.Write("Wrong path! Give right value: ");
            }


            if ((new FileInfo(argsManual[1]).Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                argsManual[0] = "-D"; //if path is folder stop collecting arguments
            }
            else
            {
                Array.Resize(ref argsManual, 3);
                argsManual[0] = "-F";

                Console.Write("Give file process option :" +
                              "\n1 to remove " +
                              "\n2 for show each ten word" +
                              "\n3 to show each third sentence\n> ");

                while (argsManual[2] == null)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            argsManual[2] = "-R";
                            break;
                        case "2":
                            argsManual[2] = "-S";
                            break;
                        case "3":
                            argsManual[2] = "-T";
                            break;
                        default:
                            Console.WriteLine("Choose right number!");
                            break;
                    }
                }


                if (argsManual[2].Equals("-R"))
                {
                    Array.Resize(ref argsManual, 5);
                    argsManual[3] = "-W";
                    Console.Write("Give word or char to remove : ");
                    argsManual[4] = Console.ReadLine()?.ToLower();
                }

            }

            return argsManual;
        }
    }
}