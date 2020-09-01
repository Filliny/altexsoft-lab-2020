using System;

namespace Recipes.Models
{

    internal interface IViewSettings
    {

        string ProgramName { get; }
        ConsoleColor Background { get; }
        Position CategoryPlace { get; }
        int TreeCellWidth { get; }
        bool AutoexpandTree { get; }
        int ListColumns { get; }
        int ListRowOffset { get; }
        int ListStartRow { get; }

    }

}