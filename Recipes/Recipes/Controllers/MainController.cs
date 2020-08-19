using Recipes.DbHandler;
using Recipes.Models;
using Recipes.Navigation;
using Recipes.Views;
using Action = Recipes.Navigation.Action;

namespace Recipes.Controllers
{

    public class MainController
    {

        public void Run()
        {
            Settings appSettings = new Settings();

            DbController storage = new DbController();
            storage.ReadTables(new DbReader());

            TopView topView = new TopView(appSettings);
            TreeNavigator treeNavigator = new TreeNavigator();
            ItemsNavigator listItemsNavigator = new ItemsNavigator();
            

            while (true)
            {

                topView.ShowMenu(string.Empty);
                RecipesSelector recipesSelector = new RecipesSelector();

                ICategory mainMenuItem = treeNavigator.Navigate(storage.TopMenu, new SimpleReader(),
                    new TreeView(appSettings), appSettings.AutoexpTree);

                if (mainMenuItem.Id == 0) //working with recipes
                {
                    ICategory recipeCategory = treeNavigator.Navigate(storage.RecipesTree.RootCategory,
                        new ArrowsReader(),
                        new TreeView(appSettings), appSettings.AutoexpTree);

                    if (recipeCategory != null)
                    {

                        topView.ShowMenu(recipeCategory.Name);
                        var recipesIn = recipesSelector.SelectRecipes(recipeCategory, storage.RecipesDb.Storage);

                        var recipeChosen =
                            listItemsNavigator.Navigate(recipesIn, new ArrowsReader(), new ItemsView(appSettings), out var action);

                        if (recipeChosen == null && action == Action.Create)
                        {
                            topView.ShowMenu(string.Empty);

                            RecipeCreateController creator = new RecipeCreateController();

                            creator.CreateRecipe(recipeCategory, storage.RecipesDb, new RecipeCreatorView(),
                                new DbWriter());
                        }

                    }

                }
                else if (mainMenuItem.Id == 1) //working with ingredients
                {
                    topView.ShowMenu(mainMenuItem.Name);

                    var ingerdientChosen = listItemsNavigator.Navigate(storage.IngredientsDb.GetListables(),
                        new ArrowsReader(), new ItemsView(appSettings), out var action);

                    if (ingerdientChosen == null && action == Action.Create)
                    {
                        topView.ShowMenu(string.Empty);


                    }

                }

            }

        }

    }

}