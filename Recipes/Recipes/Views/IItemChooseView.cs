using System.Collections.Generic;
using Recipes.Models;
using Recipes.Navigation;

namespace Recipes.Views
{

    internal interface IItemChooseView
    {

        IList<IListable> Choose(IListNavigator navigator, IItemCreator creator);

    }

}