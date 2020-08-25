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
        public Category ParentCategory { get; set; }
        public IList<Category> ChildrenCategories { get; set; }

        public Category()
        {
            ChildrenCategories = new List<Category>();
        }

        //Cos we cant use interface types in properties due JsonConverter limitations
        public List<ICategory> GetChildren()
        {
            List<ICategory> result = new List<ICategory>();

            foreach (var category in ChildrenCategories)
            {
                result.Add(category);
            }

            return result;
        }

        public ICategory GetParent()
        {
            return ParentCategory;
        }

        public void SetParent(ICategory parent)
        {
            ParentCategory = (Category) parent;
        }

    }

    public class Categories : IDataserializable
    {
        private static readonly string _filename = "Categories.json";
        public string JsonFileName => _filename;
        public Category RootCategory { get; set; }

    }

}