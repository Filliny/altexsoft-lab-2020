using System;
using Recipes.Models;

namespace Recipes.FileHandler
{

    public interface IStorageContext:IDisposable
    {

        ListModel<Category> RecipesTree { get; set; }
        ListModel<Ingredient> IngredientsFile { get; set; }
        ListModel<Recipe> RecipesFile { get; set; }
        ListModel<TopCategory> TopCategories { get; set; }

    }

}