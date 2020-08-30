using System.Collections.Generic;

namespace Recipes.Models
{

    //Tree-like type for TreeNavigator and TreeView using
    public interface ICategory: IRelational
    {

        bool Visible { get; set; }
        bool Active { get; set; }
        string Name { get; set; }
        int Position { get; set; }
        public int ParentCategoryId { get; set; }
        IList<int> ChildIds { get; set; }

    }

}