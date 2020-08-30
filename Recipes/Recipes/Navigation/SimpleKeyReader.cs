using System;

namespace Recipes.Navigation
{

    class SimpleReader : IKeyReader
    {

        public Destination GetDestination()
        {

            //Console.WriteLine("Navigate by arrows:");
            while (true)
            {
                Console.SetCursorPosition(0, 4);
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Write(" ");

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        return Destination.MoveLeft;
                    case ConsoleKey.RightArrow:
                        return Destination.MoveRight;
                    case ConsoleKey.Enter:
                        return Destination.Select;
                    case ConsoleKey.Q:
                        return Destination.Esc;
                }
            }
        }

    }

}