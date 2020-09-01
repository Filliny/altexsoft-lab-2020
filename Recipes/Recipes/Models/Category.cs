using Newtonsoft.Json;
using System.Collections.Generic;

namespace Recipes.Models
{

    public class Category :CategoryModel, ICategory
    {

        [JsonIgnore]
        public virtual bool Visible { get; set; } = false;

        [JsonIgnore]
        public bool Active { get; set; }
        public int Position { get; set; }
        public int ParentId { get; set; }

    }

}