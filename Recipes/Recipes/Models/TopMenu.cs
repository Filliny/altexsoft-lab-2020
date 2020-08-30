using System.Collections.Generic;
using Newtonsoft.Json;

namespace Recipes.Models
{

    public class TopMenu : IDataserializable
    {

        private static readonly string _filename = "topmenu.json";

        [JsonIgnore]
        public string JsonFileName => _filename;

        public List<TopCategory> TopArticles { get; set; }

        public TopMenu()
        {
            TopArticles = new List<TopCategory>();
        }

    }

}