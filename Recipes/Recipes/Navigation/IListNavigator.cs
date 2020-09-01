using System.Collections.Generic;
using Recipes.Models;

namespace Recipes.Navigation
{

    internal interface IListNavigator
    {

        IListable Navigate(IList<IListable> recipes, out Action action, bool selectable);

    }

}