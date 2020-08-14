using System;

namespace Recipes.Views
{
    class TopView
    {

        public string Header { get; set; } = "Книга рецептов";

        public void ShowMenu(string menuHead)
        {
            Console.Clear();
            int windowWidth = Console.LargestWindowWidth/2;
            Console.WindowWidth = windowWidth;

            Console.BackgroundColor = ConsoleColor.Blue;

            for (int j = 0; j < 3; j++)
            {
                Console.SetCursorPosition(0,j);

                for (int i = 0; i < windowWidth; i++)
                {
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(windowWidth/2 - Header.Length/2, 0);
            Console.WriteLine(Header);

            Console.BackgroundColor = ConsoleColor.Black;



        }

    }
}
