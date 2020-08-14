using Recipes.Models;
using System;


namespace Recipes.Views
{

    internal interface ITreePrinter
    {
        void PrintTree(ICategory rootCat, int posX, int sizeOfCell);
        void ClearView(ICategory cat, int row, int width, int col = 0);

    }

    class TreeView : ITreePrinter
    {

        public void PrintTree(ICategory rootCat, int posX, int sizeOfCell)
        {
           
            if (rootCat.GetChildren() != null)
            {
                
                PrintVisible(rootCat,0,posX, sizeOfCell);
            }
        }

      

        void PrintVisible(ICategory cat, int col,int row, int width)
        {
            Console.SetCursorPosition(col,row);
            Console.BackgroundColor = ConsoleColor.Blue;

            if (cat.Visible)
            {
                if (cat.Active)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;

                }
                Console.Write(" " + cat.Name);

                for (int i = 0; i < (width - cat.Name.Length - 1); i++) //to fill empty place to desired width
                {
                    Console.Write(" ");
                }

                Console.BackgroundColor = ConsoleColor.Blue;
            }
          
            foreach (ICategory category in cat.GetChildren())
            {
                PrintVisible(category,col+width, row, width );
                row++;
            }

            //row++;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void ClearView(ICategory cat, int row, int width, int col = 0)
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;

            // if (cat.Visible)
            // {

                Console.Write(" " + cat.Name);

                for (int i = 0; i < (width - cat.Name.Length - 1); i++) //to fill empty place to desired width
                {
                    Console.Write(" ");
                }

            // }

            foreach (ICategory category in cat.GetChildren())
            {
                ClearView(category, row, width, col + width);
                row++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
