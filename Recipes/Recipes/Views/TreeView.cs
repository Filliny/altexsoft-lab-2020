using Recipes.Models;
using System;
using System.Collections.Generic;

namespace Recipes.Views
{

    internal interface ITreePrinter<T> where T : class, ICategory
    {

        void PrintTree(IList<T> treeList);
        void ClearView(IList<T> rootCat, int col = 0);

    }

    class TreeView<T> : ITreePrinter<T> where T : class, ICategory
    {

        private readonly IViewSettings _settings;

        public TreeView(IViewSettings settings)
        {
            _settings = settings;
        }

        public void PrintTree(IList<T> treeList)
        {
            if (treeList[0].ChildIds.Count != 0)
            {
                PrintVisible(treeList, 0, 0, treeList[0].Position);
            }
        }

        void PrintVisible(IList<T> tree, int startIndex, int col, int row)
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = ConsoleColor.Blue;

            if (tree[startIndex].Visible)
            {
                if (tree[startIndex].Active)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;

                }

                Console.Write(" " + tree[startIndex].Name);

                for (int i = 0;
                    i < (_settings.TreeCellWidth - tree[startIndex].Name.Length - 1);
                    i++) //to fill empty place to desired width
                {
                    Console.Write(" ");
                }

                Console.BackgroundColor = ConsoleColor.Blue;
            }

            foreach (int categoryId in tree[startIndex].ChildIds)
            {
                PrintVisible(tree, categoryId - 1, col + _settings.TreeCellWidth, row);
                row++;
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        //Called before reprinting tree cos we don't call Console.clear
        public void ClearView(IList<T> rootCat, int col = 0)
        {
            PrintInvisible(rootCat, 0, rootCat[0].Position);
        }

        public void PrintInvisible(IList<T> cat, int startIndex, int row, int col = 0)
        {
            Console.SetCursorPosition(col, row);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Write(" " + cat[startIndex].Name);

            for (int i = 0;
                i < (_settings.TreeCellWidth - cat[startIndex].Name.Length - 1);
                i++) //to fill empty place to desired width
            {
                Console.Write(" ");
            }

            foreach (int childId in cat[startIndex].ChildIds)
            {
                PrintInvisible(cat, childId - 1, row, col + _settings.TreeCellWidth);
                row++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

    }

}