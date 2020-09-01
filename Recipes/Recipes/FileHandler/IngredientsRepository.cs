using Recipes.Models;

using System.Collections.Generic;

namespace Recipes.FileHandler
{

    class IngredientsRepository : Repository<Ingredient>
    {

        public IngredientsRepository(IList<Ingredient> collection)
            : base(collection)
        { }

    }

}