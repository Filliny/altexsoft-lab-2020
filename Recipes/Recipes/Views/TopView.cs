using System;

using Recipes.Models;

namespace Recipes.Views
{

    internal interface ITopView
    {

        public void ShowMenu();
        void ShowMenu(string menuHead);
        void ShowMenu(string rememberMenuHead, bool remember);
        
    }

    class TopView : ITopView
    {

        private string LastHead { get; set; }
        private IViewSettings Settings { get; }

        public TopView(IViewSettings settings)
        {
            Settings = settings;
        }

        //if called without message - show last remembered
        public void ShowMenu() 
        {
            ShowMenu(LastHead);
        }

        //if bool remebered = remember message
        public void ShowMenu(string rememberMenuHead, bool remember)
        {
            if(remember)
               LastHead = rememberMenuHead;

            ShowMenu(rememberMenuHead);
        }

        //Show program header with its name & menuHead as current category line
        //- solid fill 3 rows by chosen color
        public void ShowMenu(string menuHead)
        {
            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight - 20);

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