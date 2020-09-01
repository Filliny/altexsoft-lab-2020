using System.Collections.Generic;
using Recipes.Models;

namespace Recipes.Views
{

    internal interface IRecipeCreatorView
    {

        bool FillRecipe(Recipe newRecipe, IList<IListable> selectedIngredients, ICategory category);

    }

}