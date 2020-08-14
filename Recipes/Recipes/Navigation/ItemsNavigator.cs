using Recipes.Models;
using Recipes.Views;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Recipes.Navigation
{

    class ItemsNavigator
    {
        public IListable SelectedItem { get; }

        public IListable Navigate(IList<IListable> recipes, IKeyReader reader, ItemsView printer, int columnsNum)
        {
            int index = 0;

            if (recipes.Count != 0)
            {

                recipes[index].Active = true;
                printer.ShowItems(recipes, columnsNum);

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
                        return recipes[index];
                    }

                    printer.ShowItems(recipes, columnsNum);
                }
                
            }

            return null;
        }
        
    }

}