using System.Collections.Generic;

using Recipes.Models;

namespace Recipes.FileHandler
{
    interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
