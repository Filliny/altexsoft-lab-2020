using Recipes.Controllers;
using Recipes.FileHandler;
using Recipes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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

        private IRepository<Ingredient> Storage { get; }
        private ITopView TopView { get; }
        private IUnitOfWork Writer { get; }

        public ItemCreator(ITopView topView, IUnitOfWork writer)
        {
            TopView = topView;
            Writer  = writer;
            Storage = writer.Ingredients;

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
                
            var lastId = Storage.GetAll().Max(c => c.Id); ;
            newIngredient.Id = ++lastId;

            Console.Write("\nСохранить ингредиент? Д/Н : ");
            string answer = Console.ReadLine()?.ToUpper();

            if (answer != null && answer.Equals("Д"))
            {
                Storage.Create(newIngredient);

                try
                {
                    Writer.SaveFiles();
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