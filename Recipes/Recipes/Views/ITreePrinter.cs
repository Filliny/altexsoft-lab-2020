using System.Collections.Generic;
using Recipes.Models;

namespace Recipes.Views
{

    internal interface ITreePrinter<T> where T : class, ICategory
    {

        void PrintTree(IList<T> treeList);
        void ClearView(IList<T> rootCat, int col = 0);

    }

}