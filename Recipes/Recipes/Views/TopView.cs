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

        private string _lastHead;
        private readonly IViewSettings _settings;

        public TopView(IViewSettings settings)
        {
            _settings = settings;
        }

        //if called without message - show last remembered
        public void ShowMenu()
        {
            ShowMenu(_lastHead);
        }

        //if bool remebered = remember message
        public void ShowMenu(string rememberMenuHead, bool remember)
        {
            if (remember)
                _lastHead = rememberMenuHead;

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

            Console.BackgroundColor = _settings.Background;

            for (int j = 0; j < 3; j++)
            {
                Console.SetCursorPosition(0, j);

                for (int i = 0; i < windowWidth; i++)
                {
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(windowWidth / 2 - _settings.ProgramName.Length / 2, 0);
            Console.WriteLine(_settings.ProgramName);
            Console.SetCursorPosition(_settings.CategoryPlace.Left, _settings.CategoryPlace.Top);
            Console.WriteLine(menuHead);
            Console.ResetColor();

        }

    }

}