using System.Collections.Generic;

namespace Recipes.Models
{
    public class TopCategory: CategoryModel,ICategory
    {
        public bool Visible { get; set; }
        public int Position { get; set; }
        public int ParentId { get; set; }
        public bool Active { get; set; }

    }

}
