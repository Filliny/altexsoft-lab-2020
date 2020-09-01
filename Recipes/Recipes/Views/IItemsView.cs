using System.Collections.Generic;
using Recipes.Models;

namespace Recipes.Views
{

    internal interface IItemsView
    {

        //IViewSettings Settings { get; }

        void ShowItems(IList<IListable> selectedList, bool selectable);

    }

}