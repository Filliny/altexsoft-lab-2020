using Recipes.FileHandler;
using Recipes.Models;
using Recipes.Navigation;
using System;
using System.Collections.Generic;
using Action = Recipes.Navigation.Action;

namespace Recipes.Views
{

    class ItemChooseView : IItemChooseView
    {

        private readonly IUnitOfWork _storage;

        public ItemChooseView(IUnitOfWork fileWorker)
        {
            _storage = fileWorker;

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
                    selected = navigator.Navigate(_storage.Ingredients.GetListables(), out action, true);

                    if (selected == null && action == Action.Create)
                    {
                        creator.Create();
                    }

                } while (action != Action.Esc && selected != null);

                foreach (var item in _storage.Ingredients.GetListables())
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