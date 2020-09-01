using System;

namespace Recipes.Views
{

    class Description
    {

        protected string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();
            string result = "Частей";

            if (type.IsEnumDefined(enumElement))
            {
                var res = (int) Enum.Parse(type, enumElement.ToString());

                switch (res)
                {
                    case 1:
                        result = "Грамм";

                        break;
                    case 2:
                        result = "Килограмм";

                        break;
                    case 3:
                        result = "Столовых ложек";

                        break;
                    case 4:
                        result = "Чайных ложек";

                        break;
                    case 5:
                        result = "Частей";

                        break;
                    case 6:
                        result = "Пучков";

                        break;
                    case 7:
                        result = "Шт";

                        break;
                    case 8:
                        result = "Литров";

                        break;
                    case 9:
                        result = "Миллилитров";

                        break;
                }
            }

            return result;
        }

    }

}