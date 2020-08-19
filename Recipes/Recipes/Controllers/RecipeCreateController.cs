using System;
using System.Collections.Generic;
using System.Text;

using Recipes.DbHandler;
using Recipes.Models;
using Recipes.Views;

namespace Recipes.Controllers
{
    class RecipeCreateController
    {

        public void CreateRecipe(ICategory currentCategory, RecipesList recipesDb, RecipeCreatorView creatorView,DbWriter writer)
        {
            Recipe newRecipe = new Recipe();
            newRecipe.CategoryId = currentCategory.Id;
            
            creatorView.FillRecipe(newRecipe);
            recipesDb.Storage.Add(newRecipe);

            writer.WriteDbFile(recipesDb);



        }

    }
}
