using Recipes.FileHandler;
using Recipes.Models;
using System;
using System.Linq;

namespace Recipes.Views
{
    class RecipeView:Description
    {

        private readonly IUnitOfWork _storage;

        private readonly ITopView _topView;

        public RecipeView(IUnitOfWork storage, ITopView topView)
        {
            _storage = storage;
            _topView = topView;
        }

        public void ShowRecipe(IListable recipe)
        {

            var onRecipe = _storage.Recipes.GetAll().First(c => c.Id == recipe.Id);

            _topView.ShowMenu(onRecipe.Name);

            Console.SetCursorPosition(5,5);

            Console.WriteLine("Ингредиенты:\n");

            foreach (var ingredientId in onRecipe.IngredientsId)
            {
                var ingredient = _storage.Ingredients.GetAll().First(c => c.Id == ingredientId.Key);

                Console.WriteLine($"{ingredient.Name}  = {ingredientId.Value} { GetDescription(ingredient.Measure)}");
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
