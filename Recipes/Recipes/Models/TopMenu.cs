using System.Collections.Generic;

namespace Recipes.Models
{
    public class TopMenu:ICategory,IDataserializable
    {
        public string DbFilename { get; } = "topmenu.json";
        public bool Visible { get; set; }
        public bool Active { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        TopMenu ParentCategory { get; set; }

        public IList<TopMenu> ChildrenCategories { get; set; }

        public TopMenu()
        {
            ChildrenCategories = new List<TopMenu>();
        }

        public List<ICategory> GetChildren()
        {
            List<ICategory> result = new List<ICategory>();

            foreach (var category in ChildrenCategories)
            {
                result.Add((ICategory)category);
            }

            return result;
        }

        public ICategory GetParent()
        {
            return (ICategory)ParentCategory;
        }

        public void SetParent(ICategory parent)
        {
            ParentCategory = (TopMenu)parent; 
        }
        
    }



}
