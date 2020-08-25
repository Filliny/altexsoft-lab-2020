using System.Collections.Generic;

using Recipes.Controllers;
using Recipes.Models;

using System.Linq;

namespace Recipes.FileHandler
{

    class IngredientsRepository : IRepository<Ingredient>
    {

        private IStorageContext _storageContext;

        public IngredientsRepository(IStorageContext context)
        {
            _storageContext = context;
        }

        public IList<Ingredient> GetAll()
        {
            return _storageContext.IngredientsFile.IngredientsList;
        }

        public Ingredient Get(int id)
        {
            return _storageContext.IngredientsFile.IngredientsList.First(r => r.Id == id);
        }

        public void Create(Ingredient item)
        {
            _storageContext.IngredientsFile.IngredientsList.Add(item);
        }

        public void Update(Ingredient item)
        {
            Ingredient existing = _storageContext.IngredientsFile.IngredientsList.First(r => r.Id == item.Id);

            if (existing != null)
            {
                int index = _storageContext.IngredientsFile.IngredientsList.IndexOf(existing);
                _storageContext.IngredientsFile.IngredientsList.Insert(index, item);
            }

        }

        public void Delete(int id)
        {
            Ingredient existing = _storageContext.IngredientsFile.IngredientsList.First(r => r.Id == id);

            if (existing != null)
            {
                int index = _storageContext.IngredientsFile.IngredientsList.IndexOf(existing);
                _storageContext.IngredientsFile.IngredientsList.RemoveAt(index);
            }
        }

    }

}