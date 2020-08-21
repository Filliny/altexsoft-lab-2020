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
            Settings appSettings = new Settings(); //init settings  (hardcoded- can be .json too)

            DbController storage = new DbController();
            storage.ReadTables(new DbReader()); //read all tables from files to storage

            ITopView topView = new TopView(appSettings); //top menu plank view instance 

            IKeyReader arrowsFull = new ArrowsReader();   //key reader for categories and lists
            IKeyReader arrowsSimple = new SimpleReader(); //key reader for main top menu

            ITreePrinter treePrinter = new TreeView(appSettings); //tree category printer
            IItemsView listPrinter = new ItemsView(appSettings);  //list items printer

            IDbWriter dbWriter = new DbWriter();

            RecipeView recipeView = new RecipeView(storage, topView);                 //to show selected recipe
            TreeNavigator treeNavigator = new TreeNavigator();                        //tree navigator
            ListNavigator listNavigator = new ListNavigator(arrowsFull, listPrinter); //list navigator

            IItemChooseView
                ingredientChooserView =
                    new ItemChooseView(storage);                                             //view for choose ingredients new recipe 
            IRecipeCreatorView recipeCreatorView = new RecipeCreatorView(dbWriter, storage); //all recipe creator view
            IItemCreator itemCreator = new ItemCreator(topView, dbWriter, storage);          //ingredient creator view

            while (true)
            {

                topView.ShowMenu(string.Empty);

                ICategory mainMenuItem = treeNavigator.Navigate(storage.TopMenu, arrowsSimple,
                    treePrinter, appSettings.AutoexpTree);

                if (mainMenuItem.Id == 0) //working with recipes
                {
                    ICategory recipeCategory = treeNavigator.Navigate(storage.RecipesTree.RootCategory,
                        arrowsFull, treePrinter, appSettings.AutoexpTree);

                    RecipesSelector recipesSelector = new RecipesSelector(); //recipes selector for displaying

                    if (recipeCategory != null)
                    {

                        topView.ShowMenu(recipeCategory.Name);
                        var recipesIn = recipesSelector.SelectRecipes(recipeCategory, storage.RecipesDb.Storage);

                        var recipeChosen =
                            listNavigator.Navigate(recipesIn, out var action, false);

                        if (recipeChosen == null && action == Action.Create)
                        {
                            Recipe newRecipe = new Recipe();

                            topView.ShowMenu($"Категория {recipeCategory.Name} > Новый рецепт . " +
                                             $"Выберите ингридиенты c помощью Space", true);

                            var ingredients =
                                ingredientChooserView.Choose(listNavigator, itemCreator); //Get ingredients 

                            topView.ShowMenu($"Категория {recipeCategory.Name} > Новый рецепт ");

                            recipeCreatorView.FillRecipe(newRecipe, ingredients, recipeCategory);

                        }

                        if (recipeChosen != null && action == Action.Select)
                        {
                            recipeView.ShowRecipe(recipeChosen);
                        }

                    }

                }
                else if (mainMenuItem.Id == 1) //working with ingredients
                {
                    Action action;

                    do
                    {
                        topView.ShowMenu(mainMenuItem.Name);

                        var ingredientChosen = listNavigator.Navigate(storage.IngredientsDb.GetListables(),
                            out action, false);

                        if (ingredientChosen == null && action == Action.Create)
                        {
                            topView.ShowMenu(string.Empty);

                            itemCreator.Create();
                        }

                    } while (action != Action.Esc);

                }
                else if (mainMenuItem.Id == 2)
                {

                    return;
                }

            }

        }

    }

}