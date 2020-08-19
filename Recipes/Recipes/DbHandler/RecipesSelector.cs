using System;

using Recipes.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Recipes.DbHandler
{

    public class RecipesSelector
    {

        public List<IListable> Selected { get; }

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

            //Selected.Sort((x,y)=>String.Compare(x.Name,y.Name,StringComparison.CurrentCulture));
            //moved IComparer to interface
            Selected.Sort();
            return Selected;
        }

    }

}