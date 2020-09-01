using Recipes.FileHandler;
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

            IFileManager fileManager = new FileManager();

            UnitOfWork fileUnit = new UnitOfWork(fileManager);
            fileUnit.ReadFiles(); //read all tables from files to storage

            //FileWriters<>

            ITopView topView = new TopView(appSettings); //top menu plank view instance 

            IKeyReader arrowsFull = new ArrowsKeyReader(); //key reader for categories and lists
            IKeyReader arrowsSimple = new SimpleReader();  //key reader for main top menu

            ITreePrinter<TopCategory> topPrinter = new TreeView<TopCategory>(appSettings);
            ITreePrinter<Category> treePrinter = new TreeView<Category>(appSettings); //tree category printer
            IItemsView listPrinter = new ItemsView(appSettings);                      //list items printer

            RecipeView recipeView = new RecipeView(fileUnit, topView); //to show selected recipe
            TreeNavigator<TopCategory> topNavigator = new TreeNavigator<TopCategory>();
            TreeNavigator<Category> treeNavigator = new TreeNavigator<Category>();    //tree navigator
            ListNavigator listNavigator = new ListNavigator(arrowsFull, listPrinter); //list navigator

            IItemChooseView
                ingredientChooserView =
                    new ItemChooseView(fileUnit);                                   //view for choose ingredients new recipe 
            IRecipeCreatorView recipeCreatorView = new RecipeCreatorView(fileUnit); //all recipe creator view
            IItemCreator itemCreator = new ItemCreator(topView, fileUnit);          //ingredient creator view

            while (true)
            {

                topView.ShowMenu(string.Empty);

                ICategory mainMenuItem = topNavigator.Navigate(fileUnit.TopMenu.GetAll(), arrowsSimple,
                    topPrinter, appSettings.AutoexpandTree);

                switch (mainMenuItem.Id)
                {
                    case 1: //working with recipes

                        ICategory recipeCategory = treeNavigator.Navigate(fileUnit.Categories.GetAll(),
                            arrowsFull, treePrinter, appSettings.AutoexpandTree);

                        RecipesSelector recipesSelector = new RecipesSelector(); //recipes selector for displaying

                        if (recipeCategory == null)
                            break;

                        topView.ShowMenu(recipeCategory.Name);

                        var recipesIn = recipesSelector.SelectRecipes(recipeCategory,
                            fileUnit.Recipes.GetAll(), fileUnit.Categories.GetAll());

                        var recipeChosen =
                            listNavigator.Navigate(recipesIn, out var action, false);

                        if (recipeChosen == null && action == Action.Create)
                        {
                            Recipe newRecipe = new Recipe();

                            topView.ShowMenu($"Категория {recipeCategory.Name} > Новый рецепт . " +
                                             $"Выберите ингридиенты c помощью Space", true);

                            var selectedIingredients =
                                ingredientChooserView.Choose(listNavigator, itemCreator); //Get ingredients 

                            topView.ShowMenu($"Категория {recipeCategory.Name} > Новый рецепт ");

                            recipeCreatorView.FillRecipe(newRecipe, selectedIingredients, recipeCategory);

                        }

                        if (recipeChosen != null && action == Action.Select)
                        {
                            recipeView.ShowRecipe(recipeChosen);
                        }

                        break;

                    case 2: // working with ingredients

                        Action action1;

                        do
                        {
                            topView.ShowMenu(mainMenuItem.Name);

                            var ingredientChosen = listNavigator.Navigate(fileUnit.Ingredients.GetListables(),
                                out action1, false);

                            if (ingredientChosen == null && action1 == Action.Create)
                            {
                                topView.ShowMenu(string.Empty);

                                itemCreator.Create();
                            }

                        } while (action1 != Action.Esc);

                        break;

                    case 3:
                        fileUnit.Dispose(true);

                        return;
                }

            }

        }

    }

}