using Recipes.Models;
using System;

namespace Recipes.Views
{
    class RecipeCreatorView
    {

        public Recipe FillRecipe(Recipe newRecipe)
        {
            Console.SetCursorPosition(1, 1);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Новый рецепт: ");

            Console.SetCursorPosition(3,5);

            Console.Write("Введите название рецепта: ");
            newRecipe.Name = Console.ReadLine();

            Console.SetCursorPosition(18, 1);
            Console.WriteLine(newRecipe.Name);
            return newRecipe;

        }

    }
}
