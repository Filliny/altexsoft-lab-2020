using System;

using Recipes.Models;

namespace Recipes.Views
{

    class TopView
    {
        private IViewSettings Settings { get; }

        public TopView(IViewSettings settings)
        {
            Settings = settings;
        }

        //Show program header with its name & menuHead as current category line
        //- solid fill 3 rows by chosen color
        public void ShowMenu(string menuHead)
        {
            Console.Clear();
            int windowWidth = Console.LargestWindowWidth / 2;
            Console.WindowWidth = windowWidth;

            Console.BackgroundColor = Settings.Background;

            for (int j = 0; j < 3; j++)
            {
                Console.SetCursorPosition(0, j);

                for (int i = 0; i < windowWidth; i++)
                {
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(windowWidth / 2 - Settings.ProgramName.Length / 2, 0);
            Console.WriteLine(Settings.ProgramName);
            Console.SetCursorPosition(Settings.CategoryPlace.Left,Settings.CategoryPlace.Top);
            Console.WriteLine(menuHead);
            Console.ResetColor();

        }

    }

}