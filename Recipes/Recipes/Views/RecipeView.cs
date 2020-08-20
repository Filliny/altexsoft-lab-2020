using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;

using Recipes.Controllers;
using Recipes.Models;

namespace Recipes.Views
{
    class RecipeView
    {
        private IDbController Storage { get; }

        private ITopView TopView { get; }

        public RecipeView(IDbController storage, ITopView topView)
        {
            Storage = storage;
            TopView = topView;
        }

        public void ShowRecipe(IListable recipe)
        {

            var onRecipe = Storage.RecipesDb.Storage.First(c => c.Id == recipe.Id);

            TopView.ShowMenu(onRecipe.Name);

            Console.SetCursorPosition(5,5);

            Console.WriteLine("Ингредиенты:\n");

            foreach (var ingredientId in onRecipe.IngredientsId)
            {
                var ingredient = Storage.IngredientsDb.IngredientsList.First(c => c.Id == ingredientId.Key);

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
