using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Recipes.Models;

namespace Recipes.DbHandler
{
    public class RecipesSelector
    {
        public List<IListable> Selected { get;  }

        public RecipesSelector()
        {
            Selected = new List<IListable>();
        }
        
        public IList<IListable> SelectRecipes(ICategory selectedCategory, IList<Recipe> Recipes)
        {
            var range = from r in Recipes where r.CategoryId == selectedCategory.Id select r;
            Selected.AddRange(range);

            foreach (var childCategory in selectedCategory.GetChildren())
            {
                SelectRecipes(childCategory, Recipes);
            }


            return Selected;
        }

    }
}
