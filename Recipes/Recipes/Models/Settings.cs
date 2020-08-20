using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Models
{

    struct Position
    {
        public int Left;
        public int Top;
    }

    internal interface IViewSettings
    {
        string ProgramName { get; set; }
        ConsoleColor Background { get; set; }
        Position CategoryPlace { get; set; }
        int TreeCellWidth { get; set; }
        bool AutoexpTree { get; set; }
        int ListColumns { get; set; }
        int ListRowOffset { get; set; }
        int ListStartRow { get; set; }
    }

    class Settings : IViewSettings
    {

        public string ProgramName { get; set; } = "Книга рецептов";
        public ConsoleColor Background { get; set; } = ConsoleColor.Blue;
        public Position CategoryPlace { get; set; } = new Position(){Left = 4, Top = 1};
        public int TreeCellWidth { get; set; } = 18;
        public bool AutoexpTree { get; set; } = true;
        public int ListColumns { get; set; } = 2;
        public int ListRowOffset { get; set; } = 5;
        public int ListStartRow { get; set; } = 4;

        
    }
}
