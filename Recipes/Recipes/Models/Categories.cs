using Newtonsoft.Json;
using System.Collections.Generic;

namespace Recipes.Models
{

    public class Categories : IDataserializable
    {

        private static readonly string _filename = "Categories.json";

        [JsonIgnore]
        public string JsonFileName => _filename;
        
        public IList<Category> CategoryList { get; set; }

        public Categories()
        {
            CategoryList = new List<Category>();
        }

    }

}