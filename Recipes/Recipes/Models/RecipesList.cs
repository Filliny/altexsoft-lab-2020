using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recipes.Models
{

    public class RecipesList : IDataserializable
    {

        private static readonly string _filename = "Recipes.json";

        [JsonIgnore]
        public string JsonFileName => _filename;

        public IList<Recipe> Storage { get; set; }

        public RecipesList()
        {
            Storage = new List<Recipe>();
        }

    }

}