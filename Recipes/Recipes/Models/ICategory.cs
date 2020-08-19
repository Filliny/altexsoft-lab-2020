using System.Collections.Generic;

namespace Recipes.Models
{
    //Tree like type for TreeNavigator and TreeView using
    public interface ICategory
    {

        bool Visible { get; set; }
        bool Active { get; set; }
        public int Id { get; set; }
        string Name { get; set; }
        int Position { get; set; }
        List<ICategory> GetChildren();
        ICategory GetParent();
        void SetParent(ICategory parent);

    }

}