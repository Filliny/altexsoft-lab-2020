using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Views
{

    class TreeView<T> : ITreePrinter<T> where T : class, ICategory
    {

        private readonly IViewSettings _settings;

        public TreeView(IViewSettings settings)
        {
            _settings = settings;
        }

        public void PrintTree(IList<T> treeList)
        {

            if (treeList.Count != 0)
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

            var childs = tree.Where(x => x.ParentId == tree[startIndex].Id);

            foreach (var categoryId in childs)
            {
                PrintVisible(tree, categoryId.Id - 1, col + _settings.TreeCellWidth, row);
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

            var childs = cat.Where(x => x.ParentId == cat[startIndex].Id);

            foreach (var childId in childs)
            {
                PrintInvisible(cat, childId.Id - 1, row, col + _settings.TreeCellWidth);
                row++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

    }

}