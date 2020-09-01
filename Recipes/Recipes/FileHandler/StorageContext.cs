using System;
using Recipes.Controllers;
using Recipes.Models;

namespace Recipes.FileHandler
{

    //Read  and holds all data tables 

    public class StorageContext : IStorageContext
    {
        private bool _disposed ;

        public ListModel<Category> RecipesTree { get; set; }
        public ListModel<Ingredient> IngredientsFile { get; set; }
        public ListModel<Recipe> RecipesFile { get; set; }
        public ListModel<TopCategory> TopCategories { get; set; }

        public StorageContext()
        {
            RecipesTree = new ListModel<Category>();
            IngredientsFile = new ListModel<Ingredient>();
            RecipesFile = new ListModel<Recipe>();
            TopCategories = new ListModel<TopCategory>();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                GC.SuppressFinalize(this);
            }
            
        }

    }

}