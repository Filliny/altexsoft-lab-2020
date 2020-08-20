using Recipes.Controllers;
using Recipes.DbHandler;
using Recipes.Models;

using System;
using System.Linq;
using System.Threading;

namespace Recipes.Views
{

    internal interface IItemCreator
    {

        void Create();

    }

    class ItemCreator : IItemCreator
    {

        private IDbController Storage { get; }
        private ITopView TopView { get; }
        private IDbWriter Writer { get; }

        public ItemCreator(ITopView topView, IDbWriter writer, IDbController storage)
        {
            TopView = topView;
            Writer  = writer;
            Storage = storage;

        }

        public void Create()
        {
            TopView.ShowMenu("Новый ингридиент. Enter - создать, Esc - отмена >");

            Ingredient newIngredient = new Ingredient();

            Console.SetCursorPosition(1, 5);
            Console.Write("Enter - создать, Esc - отмена >");

            ConsoleKey key;

            do
            {
                Console.SetCursorPosition(0, 5);
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.Escape)
                {
                    TopView.ShowMenu();

                    return;
                }

            } while (key != ConsoleKey.Enter);

            Console.SetCursorPosition(1, 7);
            Console.Write("Введите название:");
            newIngredient.Name = Console.ReadLine();

            Console.SetCursorPosition(1, 9);
            Console.Write("Выберите единицу измерения:\n");

            var measures = Enum.GetNames(typeof(Measurements));

            for (int i = 0; i < measures.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {measures[i]}");
            }

            Measurements result;

            do
            {
                Enum.TryParse(Console.ReadLine(), out result);

            } while (!Enum.IsDefined(typeof(Measurements), result));

            newIngredient.Measure = result;

            var lastId = Storage.IngredientsDb.IngredientsList.Max(c => c.Id);
            newIngredient.Id = ++lastId;

            Console.Write("\nСохранить ингредиент? Д/Н : ");
            string answer = Console.ReadLine()?.ToUpper();

            if (answer != null && answer.Equals("Д"))
            {
                Storage.IngredientsDb.IngredientsList.Add(newIngredient);

                try
                {
                    Writer.WriteDbFile(Storage.IngredientsDb);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }

                Console.WriteLine("Ингредиент сохранен!");
                Thread.Sleep(2000);

            }

            TopView.ShowMenu();

        }

    }

}