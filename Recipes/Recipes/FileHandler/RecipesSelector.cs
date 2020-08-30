using Recipes.Models;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.FileHandler
{

    public class RecipesSelector
    {

        private readonly List<IListable> _selected;

        public RecipesSelector()
        {
            _selected = new List<IListable>();
        }

        public IList<IListable> SelectRecipes(ICategory selectedCategory, IList<Recipe> recipes, IList<Category> categoriesList)
        {
            var range = from r in recipes where r.CategoryId == selectedCategory.Id select r;
            _selected.AddRange(range);

            foreach (int childCategoryId in categoriesList[selectedCategory.Id-1].ChildIds)
            {
                
                SelectRecipes(categoriesList[childCategoryId-1], recipes, categoriesList);
            }

            //Selected.Sort((x,y)=>String.Compare(x.Name,y.Name,StringComparison.CurrentCulture));
            //moved IComparer to interface
            _selected.Sort();

            return _selected;
        }

    }

}