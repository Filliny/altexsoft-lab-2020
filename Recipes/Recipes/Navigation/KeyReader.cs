using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Navigation
{

    enum Destination
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Select,
        Esc
    }

    internal interface IKeyReader
    {

        Destination GetDestination();

    }

    class ArrowsReader : IKeyReader
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
                    case ConsoleKey.UpArrow:
                        return Destination.MoveUp;
                    case ConsoleKey.DownArrow:
                        return Destination.MoveDown;
                    case ConsoleKey.LeftArrow:
                        return Destination.MoveLeft;
                    case ConsoleKey.RightArrow:
                        return Destination.MoveRight;
                    case ConsoleKey.Enter:
                        return Destination.Select;
                    case ConsoleKey.Escape:
                        return Destination.Esc;
                }
            }
        }
    }

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
                }
            }
        }
        
    }
}
