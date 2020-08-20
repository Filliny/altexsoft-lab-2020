using Recipes.Controllers;
using Recipes.DbHandler;
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

        bool FillRecipe(Recipe newRecipe, IList<IListable> ingredients, ICategory category);

    }

    class RecipeCreatorView : IRecipeCreatorView
    {

        private IDbController Storage { get; }
        private IDbWriter Writer { get; }

        public RecipeCreatorView(IDbWriter writer, IDbController storage)
        {
            Writer  = writer;
            Storage = storage;

        }

        public bool FillRecipe(Recipe newRecipe, IList<IListable> ingredients, ICategory category)
        {
            Console.SetCursorPosition(1, 4);
            Console.WriteLine("Укажите количество ингридиентов: \n");

            foreach (var ingredient in ingredients)
            {
                var realIngr = Storage.IngredientsDb.IngredientsList.First(c => c.Id == ingredient.Id);

                Console.Write($"{realIngr.Name} количество, {Enum.GetName(typeof(Measurements), realIngr.Measure)} = ");

                IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");
                decimal quantity;
                bool result;

                do
                {
                    result = decimal.TryParse(Console.ReadLine(), NumberStyles.Number, provider, out quantity);

                } while (!result && quantity == 0);

                newRecipe.IngredientsId.Add(realIngr.Id, quantity);

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
            var lastId = Storage.RecipesDb.Storage.Max(c => c.Id);
            newRecipe.Id = ++lastId;

            Console.Write("\nСохранить рецепт? Д/Н : ");
            string answer = Console.ReadLine()?.ToUpper();

            if (answer != null && answer.Equals("Д"))
            {
                Storage.RecipesDb.Storage.Add(newRecipe);

                try
                {
                    Writer.WriteDbFile(Storage.RecipesDb);
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