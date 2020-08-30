using Recipes.FileHandler;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Recipes.Views
{

    internal interface IRecipeCreatorView
    {

        bool FillRecipe(Recipe newRecipe, IList<IListable> selectedIngredients, ICategory category);

    }

    class RecipeCreatorView : Description, IRecipeCreatorView
    {

        private readonly IUnitOfWork _storage;

        public RecipeCreatorView(IUnitOfWork storage)
        {
            _storage = storage;

        }

        public bool FillRecipe(Recipe newRecipe, IList<IListable> selectedIngredients, ICategory category)
        {
            Console.SetCursorPosition(1, 4);
            Console.WriteLine("Укажите количество ингридиентов: \n");

            foreach (var ingredient in selectedIngredients)
            {
                var realIngredient = _storage.Ingredients.GetAll().First(c => c.Id == ingredient.Id);

                Console.Write($"{realIngredient.Name} количество, {GetDescription(realIngredient.Measure)} = ");

                IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
                decimal quantity;
                bool result;

                do
                {
                    result = decimal.TryParse(Console.ReadLine(), NumberStyles.Number, provider, out quantity);

                } while (!result && quantity == 0);

                newRecipe.IngredientsId.Add(realIngredient.Id, quantity);

            }

            Console.Write("\n    Введите название рецепта: ");
            newRecipe.Name = Console.ReadLine();

            Console.Write($"\n   Новый рецепт -  {newRecipe.Name}\n\n");

            Console.Write("    Введите описание рецепта: ");
            newRecipe.Explanation = Console.ReadLine();

            Console.Write("\n\n    Введите шаги приготовления(Esc для завершения): ");

            int step = 1;

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.Write($"\nШаг {step} : ");

                string stepText = Console.ReadLine();
                newRecipe.Steps.Add(step + " . " + stepText);
                step++;
            }

            newRecipe.CategoryId = category.Id;
            var lastId = _storage.Recipes.GetAll().Max(c => c.Id);
            newRecipe.Id = ++lastId;

            Console.Write("\nСохранить рецепт? Д/Н : ");
            string answer = Console.ReadLine()?.ToUpper();

            if (answer != null && answer.Equals("Д"))
            {
                _storage.Recipes.Create(newRecipe);

                try
                {
                    _storage.SaveFiles();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }

                Console.WriteLine("  Рецепт сохранен!");
                Thread.Sleep(2000);
            }

            return true;
        }

    }

}