using Recipes.Models;

namespace Recipes.Controllers
{

    public interface IStorageContext
    {

        Categories RecipesTree { get; set; }
        Ingredients IngredientsFile { get; set; }
        RecipesList RecipesFile { get; set; }
        TopMenu TopCategories { get; set; }
        void Dispose();

    }

}