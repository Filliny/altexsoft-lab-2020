using Recipes.Controllers;
using Recipes.Models;
using System;
using System.Linq;

namespace Recipes.Views
{
    class RecipeView
    {
        private IStorageContext Storage { get; }

        private ITopView TopView { get; }

        public RecipeView(IStorageContext storage, ITopView topView)
        {
            Storage = storage;
            TopView = topView;
        }

        public void ShowRecipe(IListable recipe)
        {

            var onRecipe = Storage.RecipesFile.Storage.First(c => c.Id == recipe.Id);

            TopView.ShowMenu(onRecipe.Name);

            Console.SetCursorPosition(5,5);

            Console.WriteLine("Ингредиенты:\n");

            foreach (var ingredientId in onRecipe.IngredientsId)
            {
                var ingredient = Storage.IngredientsFile.IngredientsList.First(c => c.Id == ingredientId.Key);

                Console.WriteLine($"{ingredient.Name}  = {ingredientId.Value} {ingredient.Measure}");
            }

            Console.WriteLine("\n     Описание\n");
            Console.WriteLine(onRecipe.Explanation+"\n\n");

            Console.WriteLine("     Шаги приготовления:\n");

            foreach (var step in onRecipe.Steps)
            {
                Console.WriteLine(step);
            }


            ConsoleKey key;
            Console.WriteLine("\n\nНажмите Esc для выхода >");
            do
            {
                key = Console.ReadKey().Key;

            } while (key != ConsoleKey.Escape);
        }
    }
}
