using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Recipes.Models
{

    class Category
    {
        
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual Category Parent { get; set; }
       
        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<Recipes> Products { get; set; }

        public Category()
        {
            Children = new List<Category>();
            Products = new List<Recipes>();
        }
    }

    struct Categories
    {

    }


}
