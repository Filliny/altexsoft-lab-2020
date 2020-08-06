using ConsoleApp.InputWorkers;
using PowerArgs;
using System;
using ConsoleApp.FileHelpers;
using ConsoleApp.FileProcess;


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
                Menu menuArgs = new Menu();
                args = menuArgs.GetArgs();
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

            Processor argsProcessor = new Processor();

            try
            {
                argsProcessor.Process(parsed,new Counter(), 
                    new Remover(), new Reverser(), new FileBrowser());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}