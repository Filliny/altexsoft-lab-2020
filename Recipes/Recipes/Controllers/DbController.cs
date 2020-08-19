using Recipes.DbHandler;
using Recipes.Models;

namespace Recipes.Controllers
{
    //Read  and holds all data tables 
    public class DbController
    {
        
        public Categories RecipesTree { get; set; }
        public Ingredients IngredientsDb { get; set; }
        public RecipesList RecipesDb { get; set; }
        public TopMenu TopMenu { get; set; }

        //Read all tables by generic method of dbReader class
        public void ReadTables(IDbReader dbReader)
        {
            RecipesTree     = (Categories) dbReader.ReadDb<Categories>();
            IngredientsDb = (Ingredients) dbReader.ReadDb<Ingredients>();
            RecipesDb       = (RecipesList) dbReader.ReadDb<RecipesList>();
            TopMenu         = (TopMenu) dbReader.ReadDb<TopMenu>();
        }

    }

}