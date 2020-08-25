using System;
using System.Collections.Generic;
using Recipes.Controllers;
using Recipes.FileHandler;
using Recipes.Models;
using Recipes.Navigation;
using Action = Recipes.Navigation.Action;

namespace Recipes.Views
{

    internal interface IItemChooseView
    {

        IList<IListable> Choose(IListNavigator navigator, IItemCreator creator);

    }

    class ItemChooseView : IItemChooseView
    {

        private IStorageContext Storage { get; }

        public ItemChooseView(IUnitOfWork fileWorker)
        {
            Storage = fileWorker.Context;

        }

        public IList<IListable> Choose(IListNavigator navigator, IItemCreator creator)
        {

            List<IListable> selectedResult = new List<IListable>();
            Action action;
            IListable selected;

            do
            {

                do
                {
                    selected = navigator.Navigate(Storage.IngredientsFile.GetListables(), out action, true);

                    if (selected == null && action == Action.Create)
                    {
                        creator.Create();
                    }

                } while (action != Action.Esc && selected != null);

                foreach (var item in Storage.IngredientsFile.GetListables())
                {
                    if (item.Selected)
                    {
                        selectedResult.Add(item);
                        item.Selected = false;
                    }
                }

                if (selectedResult.Count == 0)
                {
                    Console.SetCursorPosition(5, Console.WindowHeight - 5);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Вы не выбрали позиции!  ");
                    Console.ResetColor();
                }

            } while (selectedResult.Count == 0);

            return selectedResult;
        }

    }

}