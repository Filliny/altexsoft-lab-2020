using ConsoleApp.Instant;
using ConsoleApp.Statics;
using PowerArgs;
using System;


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
            
            Menu.Process(parsed);
        }
    }





   

    


   


 
    
}