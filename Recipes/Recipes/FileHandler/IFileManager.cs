using System.Collections.Generic;

namespace Recipes.FileHandler
{

    public interface IFileManager
    {
        IList<T> ReadFile<T>();

        void WriteFile<T>(IList<T> savingInstance);
    }

}