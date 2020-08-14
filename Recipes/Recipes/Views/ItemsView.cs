using Recipes.Models;
using System;
using System.Collections.Generic;

namespace Recipes.Views
{

    class ItemsView
    {

        public void ShowItems(IList<IListable> selectedList, int columnsNum)
        {
            Console.SetCursorPosition(1, 4);
            
            int rowWidth = Console.WindowWidth / (columnsNum+1);
            int rowsInCol = (selectedList.Count-(selectedList.Count % columnsNum))/(columnsNum ) ;

            foreach (var item in selectedList)
            {
                int index = selectedList.IndexOf(item);
                int column =(int) Math.Ceiling((decimal)(selectedList.IndexOf(item) / rowsInCol)) ;
                int row = selectedList.IndexOf(item) - (rowsInCol *column) + 4;
                Console.WriteLine(" ");
                Console.SetCursorPosition(column * rowWidth + 5, row);

                if (item.Active)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }

                //Console.WriteLine(" ");
                Console.WriteLine(" " + item.Name);
                Console.BackgroundColor = ConsoleColor.Black;

            }
        }

    }

}