using Recipes.Models;
using Recipes.Views;
using System.Collections.Generic;

namespace Recipes.Navigation
{

    class ListNavigator : IListNavigator
    {

        private readonly IKeyReader _keyReader;
        private readonly IItemsView _printer;

        public ListNavigator(IKeyReader keyReader, IItemsView printer)
        {
            _keyReader = keyReader;
            _printer   = printer;
        }

        //Navigate by list of items. Returns  selected item or null if selected other action 
        public IListable Navigate(IList<IListable> recipes, out Action action, bool selectable)
        {
            List<IListable> sortedList = (List<IListable>) recipes;
            sortedList.Sort();

            int index = 0;

            if (recipes.Count != 0)
            {

                recipes[index].Active = true;
                _printer.ShowItems(recipes, selectable);

                while (true)
                {
                    Destination destination = _keyReader.GetDestination();

                    if (destination == Destination.MoveDown)
                    {
                        index++;

                        if (index < recipes.Count)
                        {
                            recipes[index].Active = true;

                            if (index > 0)
                                recipes[index - 1].Active = false;
                        }
                        else
                        {
                            recipes[index - 1].Active = false;
                            index                     = 0;
                            recipes[index].Active     = true;
                        }
                    }
                    else if (destination == Destination.MoveUp)
                    {
                        index--;

                        if (index >= 0)
                        {

                            recipes[index].Active = true;

                            if (index < recipes.Count)
                                recipes[index + 1].Active = false;
                        }
                        else
                        {
                            recipes[index + 1].Active = false;
                            index                     = recipes.Count - 1;
                            recipes[index].Active     = true;
                        }

                    }
                    else if (destination == Destination.Select)
                    {
                        recipes[index].Active = false;

                        action = Action.Select;

                        return recipes[index];

                    }
                    else if (destination == Destination.Mark && selectable)
                    {
                        recipes[index].Selected = recipes[index].Selected != true;

                        action = Action.Select;

                    }
                    else if (destination == Destination.Esc)
                    {
                        recipes[index].Active = false;
                        action                = Action.Esc;

                        return null;

                    }
                    else if (destination == Destination.Create)
                    {
                        recipes[index].Active = false;
                        action                = Action.Create;

                        return null;
                    }

                    _printer.ShowItems(recipes, selectable);
                }

            }

            action = Action.None;

            return null;
        }

    }

}