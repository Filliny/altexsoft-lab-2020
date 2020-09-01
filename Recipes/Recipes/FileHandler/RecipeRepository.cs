using Recipes.Models;

using System.Collections.Generic;

namespace Recipes.FileHandler
{

    class RecipeRepository : Repository<Recipe>
    {

        public RecipeRepository(IList<Recipe> collection)
            : base(collection)
        { }

    }

}