using System.Collections.Generic;

namespace Recipes.Models
{
    public class TopCategory:ICategory
    {
        public bool Visible { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public int ParentCategoryId { get; set; }

        public IList<int> ChildIds { get; set; }

        public TopCategory()
        {
            ChildIds = new List<int>();
        }

        public List<int> GetChildren()
        {
            List<int> result = new List<int>();

            foreach (var category in ChildIds)
            {
                result.Add(category);
            }

            return result;
        }

        public int GetParent()
        {
            return ParentCategoryId;
        }

        public void SetParent(int parent)
        {
            ParentCategoryId = parent; 
        }
        
    }

}
