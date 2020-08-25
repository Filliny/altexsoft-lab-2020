using Recipes.Models;
using System;

namespace Recipes.Controllers
{

    //Read  and holds all data tables 
    public interface IStorageContext
    {

        Categories RecipesTree { get; set; }
        Ingredients IngredientsFile { get; set; }
        RecipesList RecipesFile { get; set; }
        TopMenu TopMenu { get; set; }
        void Dispose();

    }

    public class StorageContext : IStorageContext
    {
        private bool disposed = false;

        public Categories RecipesTree { get; set; }
        public Ingredients IngredientsFile { get; set; }
        public RecipesList RecipesFile { get; set; }
        public TopMenu TopMenu { get; set; }

        public void Dispose()
        {
            if (!this.disposed == false)
            {
                disposed = true;
                GC.SuppressFinalize(this);
            }
            
        }

    }

}