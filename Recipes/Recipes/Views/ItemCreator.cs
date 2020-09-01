using Recipes.FileHandler;
using Recipes.Models;
using System;
using System.Linq;
using System.Threading;

namespace Recipes.Views
{

    class ItemCreator : Description, IItemCreator
    {

        private readonly IRepository<Ingredient> _storage;
        private readonly ITopView _topView;
        private readonly IUnitOfWork _fileSaver;

        public ItemCreator(ITopView topView, IUnitOfWork fileUnit)
        {
            _topView   = topView;
            _fileSaver = fileUnit;
            _storage   = fileUnit.Ingredients;

        }

        public void Create()
        {
            _topView.ShowMenu("Новый ингридиент. Enter - создать, Esc - отмена >");

            Console.SetCursorPosition(1, 5);
            Console.Write("Enter - создать, Esc - отмена >");

            ConsoleKey key;

            do
            {
                Console.SetCursorPosition(0, 5);
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.Escape)
                {
                    _topView.ShowMenu();

                    return;
                }

            } while (key != ConsoleKey.Enter);

            Console.SetCursorPosition(1, 7);

            Ingredient newIngredient = new Ingredient();

            Console.Write("Введите название:");
            newIngredient.Name = Console.ReadLine();

            Console.SetCursorPosition(1, 9);
            Console.Write("Выберите единицу измерения:\n");

            var measures = Enum.GetValues(typeof(Measurements));

            foreach (Measurements VARIABLE in measures)
            {

                Console.WriteLine($"{(int) VARIABLE} - {GetDescription(VARIABLE)}");
            }

            Measurements result;

            do
            {
                Enum.TryParse(Console.ReadLine(), out result);

            } while (!Enum.IsDefined(typeof(Measurements), result));

            newIngredient.Measure = result;

            var lastId = _storage.GetAll().Max(c => c.Id);
            ;
            newIngredient.Id = ++lastId;

            Console.Write("\nСохранить ингредиент? Д/Н : ");
            string answer = Console.ReadLine()?.ToUpper();

            if (answer != null && answer.Equals("Д"))
            {
                _storage.Create(newIngredient);

                try
                {
                    _fileSaver.SaveFiles();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw;
                }

                Console.WriteLine("Ингредиент сохранен!");
                Thread.Sleep(2000);

            }

            _topView.ShowMenu();

        }

    }

}