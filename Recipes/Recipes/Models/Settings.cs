using System;

namespace Recipes.Models
{

    struct Position
    {

        public int Left;
        public int Top;

    }

    class Settings : IViewSettings
    {

        private static string _programName = "Книга рецептов";
        private static ConsoleColor _background = ConsoleColor.Blue;
        private static readonly Position _categoryPlace = new Position() {Left = 4, Top = 1};
        private static int _treeCellWidth = 18;
        private static int _listColumns = 2;
        private static int _listRowOffset = 5;
        private static int _listStartRow = 4;

        public string ProgramName => _programName;
        public ConsoleColor Background => _background;
        public Position CategoryPlace => _categoryPlace;
        public int TreeCellWidth => _treeCellWidth;
        public bool AutoexpandTree { get; } = true;
        public int ListColumns => _listColumns;
        public int ListRowOffset => _listRowOffset;
        public int ListStartRow => _listStartRow;

    }

}