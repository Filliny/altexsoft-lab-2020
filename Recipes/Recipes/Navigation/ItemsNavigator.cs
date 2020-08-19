using System;

using Recipes.Models;
using Recipes.Views;
using System.Collections.Generic;

namespace Recipes.Navigation
{

    class ItemsNavigator
    {
        //Nagigate by list of items. Returns  selected item or null if selected other action 
        public IListable Navigate(IList<IListable> recipes, IKeyReader reader, ItemsView printer, out Action action  )
        {
            int index = 0;

            if (recipes.Count != 0)
            {

                recipes[index].Active = true;
                printer.ShowItems(recipes);

                while (true)
                {
                    Destination destination = reader.GetDestination();

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

                        action = Action.None;
                        return recipes[index];

                    }
                    else if(destination == Destination.Esc)
                    {
                        recipes[index].Active = false;
                        action = Action.None;
                        return null;

                    }
                    else if (destination == Destination.Create)
                    {
                        recipes[index].Active = false;
                        action                = Action.Create;
                        return null;
                    }
                    printer.ShowItems(recipes);
                }

            }

            action = Action.None;
            return null;
        }

    }

}