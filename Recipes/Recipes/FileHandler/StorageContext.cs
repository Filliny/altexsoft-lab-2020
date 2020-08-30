using System;
using Recipes.Controllers;
using Recipes.Models;

namespace Recipes.FileHandler
{

    //Read  and holds all data tables 

    public class StorageContext : IStorageContext
    {
        private bool _disposed = false;

        public Categories RecipesTree { get; set; }
        public Ingredients IngredientsFile { get; set; }
        public RecipesList RecipesFile { get; set; }
        public TopMenu TopCategories { get; set; }

        public void Dispose()
        {
            if (!this._disposed == false)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
            }
            
        }

    }

}