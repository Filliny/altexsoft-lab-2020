using Recipes.Models;
using System.Collections.Generic;

namespace Recipes.FileHandler
{

    class CategoriesRepository : Repository<Category>
    {

        public CategoriesRepository(IList<Category> collection)
            : base(collection)
        { }

    }

}