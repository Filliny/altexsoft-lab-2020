using Newtonsoft.Json;
using System.Collections.Generic;

namespace Recipes.Models
{

    public class Category : ICategory
    {

        [JsonIgnore]
        public virtual bool Visible { get; set; } = false;

        [JsonIgnore]
        public bool Active { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int ParentCategoryId { get; set; }
        public IList<int> ChildIds { get; set; }

        public Category()
        {
            ChildIds = new List<int>();
        }

    }

}