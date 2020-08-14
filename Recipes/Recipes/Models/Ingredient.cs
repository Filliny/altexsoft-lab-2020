using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Recipes.Models
{

    public class Ingredient:IListable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public bool Active { get; set; }
        public Measurements Measure { get; set; }


    }


    public class Ingredients: IDataserializable
    {
        public string DbFilename { get; } = "Ingredients.json";
        public IList<Ingredient> IngredientsList { get; set; }

        public Ingredients()
        {
            IngredientsList = new List<Ingredient>();
        }

    }

}