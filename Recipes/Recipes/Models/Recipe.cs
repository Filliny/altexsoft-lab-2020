using System;
using System.Collections.Generic;

namespace Recipes.Models
{

    public class Recipe : CategoryModel, IListable
    {

        public int CategoryId { get; set; }
        public bool Active { get; set; }
        public bool Selected { get; set; }
        public string Explanation { get; set; }
        public Dictionary<int, decimal> IngredientsId { get; set; }
        public List<string> Steps { get; set; }

        public Recipe()
        {
            IngredientsId = new Dictionary<int, decimal>();
            Steps         = new List<string>();
        }

        public int CompareTo(object obj)
        {

            Recipe other = (Recipe) obj;

            return string.Compare(this.Name, other.Name, StringComparison.CurrentCulture);

        }

    }

}