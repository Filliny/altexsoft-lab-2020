using Recipes.DbHandler;
using Recipes.Models;

namespace Recipes.Controllers
{

    public class DbController
    {

        public Categories RecipesTree { get; set; }
        public Ingredients IngredientsList { get; set; }
        public RecipesList RecipesDb { get; set; }
        public TopMenu TopMenu { get; set; }

        public void ReadTables(IDbReader dbReader)
        {
            RecipesTree     = (Categories) dbReader.ReadDb<Categories>();
            IngredientsList = (Ingredients) dbReader.ReadDb<Ingredients>();
            RecipesDb       = (RecipesList) dbReader.ReadDb<RecipesList>();
            TopMenu         = (TopMenu) dbReader.ReadDb<TopMenu>();
        }

    }

}