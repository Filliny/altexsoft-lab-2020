using System.Collections.Generic;

namespace Recipes.Models
{
    public class ListModel<T>
    {

        public IList<T> ItemsList;

        public ListModel()
        {
            ItemsList = new List<T>();
            
        }

    }
}
