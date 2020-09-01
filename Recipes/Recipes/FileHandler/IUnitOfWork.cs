namespace Recipes.FileHandler
{

    internal interface IUnitOfWork
    {

        void ReadFiles();
        void SaveFiles();
        
        RecipeRepository Recipes { get; }
        IngredientsRepository Ingredients { get; }
        CategoriesRepository Categories { get; }
        TopMenuRepository TopMenu { get; }

    }

}