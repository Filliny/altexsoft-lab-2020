namespace Recipes.Models
{

    //Tree-like type for TreeNavigator and TreeView using
    public interface ICategory
    {

        int Id { get; set; }
        bool Visible { get; set; }
        bool Active { get; set; }
        string Name { get; set; }
        int Position { get; set; }
        public int ParentId { get; set; }

    }

}