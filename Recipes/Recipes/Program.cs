using Recipes.Controllers;
using System;

namespace Recipes
{

    class Program
    {

        static void Main(string[] args)
        {

            // DbTester test = new DbTester();
            // test.SetDb();


            MainController program = new MainController();

            try
            {
                program.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

        }

    }

}