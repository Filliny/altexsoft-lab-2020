using System;

namespace Recipes.Models
{

    public class Ingredient : CategoryModel, IListable
    {
        public bool Active { get; set; }
        public bool Selected { get; set; }
        public Measurements Measure { get; set; }

        public int CompareTo(object obj)
        {
            Ingredient other = (Ingredient) obj;

            return string.Compare(this.Name, other.Name, StringComparison.CurrentCulture);
        }

    }

}