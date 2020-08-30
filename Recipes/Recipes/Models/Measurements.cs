using System.ComponentModel;

namespace Recipes.Models
{

    public enum Measurements
    {
        [Description("Грамм")]
        Gram = 1,
        [Description("Килограмм")]
        Kilogram,
        [Description("Столовых ложек")]
        TableSpoon,
        [Description("Чайных ложек")]
        TeaSpoon,
        [Description("Частей")]
        Parts,
        [Description("Пучков")]
        Bunch,
        [Description("Шт")]
        Piece,
        [Description("Литров")]
        Liter,
        [Description("Миллилитров")]
        Milliliter

        

    }

}