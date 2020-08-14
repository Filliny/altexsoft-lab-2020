using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using Newtonsoft.Json;

namespace Recipes.Models
{

    public interface IListable
    {

        int Id { get; set; }
        string Name { get; set; }

        [JsonIgnore]
        bool Active { get; set; }

    }

    public class Recipe : IListable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool Active { get; set; }
        
        public string Explanation { get; set; }

        public List<int> IngredientsId { get; set; }

        public string[] Steps { get; set; }

        public Recipe()
        {
            IngredientsId = new List<int>();
        }
        
    }


    public class RecipesList: IDataserializable
    {

        public string DbFilename { get; } = "Recipes.json";
        public IList<Recipe> Storage { get; set; }

        public RecipesList()
        {
            Storage = new List<Recipe>();
        }

    }
}