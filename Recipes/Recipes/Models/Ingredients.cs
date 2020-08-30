using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recipes.Models
{

    public class Ingredients : IDataserializable
    {

        private static readonly string _filename = "Ingredients.json";

        [JsonIgnore]
        public string JsonFileName => _filename;

        public IList<Ingredient> IngredientsList { get; set; }

        public Ingredients()
        {
            IngredientsList = new List<Ingredient>();
        }

    }

}