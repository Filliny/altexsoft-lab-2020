using Recipes.DbHandler;
using Recipes.Navigation;
using Recipes.Views;
using System;

using Recipes.Models;

namespace Recipes.Controllers
{

    public class MainController
    {

        public void Run()
        {
            DbController Storage = new DbController();
            Storage.ReadTables(new DbReader());

            TreeNavigator treeNavigator = new TreeNavigator();
          
            ItemsNavigator listItemsNavigator = new ItemsNavigator();
            TopView topView = new TopView();

            while (true)
            {
                topView.ShowMenu(string.Empty);
                RecipesSelector recipesSelector = new RecipesSelector();

                ICategory mainMenuItem = treeNavigator.Navigate(Storage.TopMenu, new SimpleReader(), 
                    new TreeView(), true, 1, 14);

                if (mainMenuItem.Id == 0 ) //working with recipes
                {
                    ICategory recipeCategory = treeNavigator.Navigate(Storage.RecipesTree.RootCategory, new ArrowsReader(), 
                        new TreeView(), true, 3, 18);

                    if (recipeCategory != null)
                    {
                        topView.ShowMenu(recipeCategory.Name);
                        var recipesIn = recipesSelector.SelectRecipes(recipeCategory, Storage.RecipesDb.Storage);

                        var recipeChosen =
                            listItemsNavigator.Navigate(recipesIn, new ArrowsReader(), new ItemsView(), 2);

                        //if(recipeChosen)


                    }


                }
                else if (mainMenuItem.Id == 1) //working with ingredients
                {
                    
                }

                









            }

             //var sel = nav.Navigate(Storage.TopMenu, new SimpleReader(), new TreeView(), true, 1, 14);

            //Console.WriteLine(File.ReadAllText("Recipes.json"));

            //var selected = nav.Navigate(Storage.RecipesTree.RootCategory, new ArrowsReader(), new TreeView(), true, 3, 18);

            //RecipesSelector selector = new RecipesSelector();
            


            // ItemsNavigator inav = new ItemsNavigator();
            // inav.Navigate(res, new ArrowsReader(), new ItemsView(),2);



        }

    }

}