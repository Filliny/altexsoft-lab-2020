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

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

    }


    public class Ingredients: IDataserializable
    {
        public string DbFilename { get; } = "Ingredients.json";
        public IList<Ingredient> IngredientsList { get; set; }

        public Ingredients()
        {
            IngredientsList = new List<Ingredient>();
        }

        public IList<IListable> GetListables()
        {
            IList<IListable> result = new List<IListable>();

            foreach (var ingredient in IngredientsList)
            {
                result.Add(ingredient);
            }

            return result;
        }
    }

}