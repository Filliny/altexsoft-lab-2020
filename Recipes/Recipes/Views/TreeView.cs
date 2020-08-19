using Recipes.Models;

using System;

namespace Recipes.Views
{

    internal interface ITreePrinter
    {

        void PrintTree(ICategory rootCat);
        void ClearView(ICategory rootCat, int col = 0);

    }

    class TreeView : ITreePrinter
    {

        private IViewSettings Settings { get; }
        public TreeView(IViewSettings settings)
        {
            Settings = settings;
        }

        public void PrintTree(ICategory rootCat )
        {
            if (rootCat.GetChildren() != null)
            {
                PrintVisible(rootCat, 0, rootCat.Position);
            }
        }

        void PrintVisible(ICategory cat, int col,int row)
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = ConsoleColor.Blue;

            if (cat.Visible)
            {
                if (cat.Active)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;

                }

                Console.Write(" " + cat.Name);

                for (int i = 0; i < (Settings.TreeCellWidth - cat.Name.Length - 1); i++) //to fill empty place to desired width
                {
                    Console.Write(" ");
                }

                Console.BackgroundColor = ConsoleColor.Blue;
            }

            foreach (ICategory category in cat.GetChildren())
            {
                PrintVisible(category, col + Settings.TreeCellWidth, row);
                row++;
            }

            //row++;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Called before reprinting tree cos we don't call Console.clear
        public void ClearView(ICategory rootCat, int col = 0)
        {
            PrintInvisible(rootCat,rootCat.Position);
        }


        public void PrintInvisible(ICategory cat, int row , int col = 0)
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write(" " + cat.Name);

            for (int i = 0; i < (Settings.TreeCellWidth - cat.Name.Length - 1); i++) //to fill empty place to desired width
            {
                Console.Write(" ");
            }

            foreach (ICategory category in cat.GetChildren())
            {
                PrintInvisible(category, row, col + Settings.TreeCellWidth);
                row++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

    }

}