using System.Collections.Generic;

using Recipes.Controllers;
using Recipes.Models;
using System.Linq;

namespace Recipes.FileHandler
{

    class RecipeRepository : IRepository<Recipe>
    {

        private IStorageContext _storageContext;

        public RecipeRepository(IStorageContext context)
        {
            _storageContext = context;
        }

        public IList<Recipe> GetAll()
        {
            return _storageContext.RecipesFile.Storage;
        }

        public Recipe Get(int id)
        {
            return _storageContext.RecipesFile.Storage.First(r => r.Id == id);
        }

        public void Create(Recipe item)
        {
            _storageContext.RecipesFile.Storage.Add(item);
        }

        public void Update(Recipe item)
        {
            Recipe existing = _storageContext.RecipesFile.Storage.First(r => r.Id == item.Id);

            if (existing != null)
            {
                int index = _storageContext.RecipesFile.Storage.IndexOf(existing);
                _storageContext.RecipesFile.Storage.Insert(index, item);
            }

        }

        public void Delete(int id)
        {
            Recipe existing = _storageContext.RecipesFile.Storage.First(r => r.Id == id);

            if (existing != null)
            {
                int index = _storageContext.RecipesFile.Storage.IndexOf(existing);
                _storageContext.RecipesFile.Storage.RemoveAt(index);
            }
        }

    }

}