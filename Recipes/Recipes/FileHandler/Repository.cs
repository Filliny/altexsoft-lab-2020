using Recipes.Models;

using System.Collections.Generic;
using System.Linq;

namespace Recipes.FileHandler
{

    abstract class Repository<T> : IRepository<T> where T :  CategoryModel
    {

        private readonly IList<T> _collection;

        protected Repository(IList<T> collection)
        {
            _collection = collection;
        }

        public IList<T> GetAll()
        {
            return _collection;

        }

        public IList<IListable> GetListables()
        {
            IList<IListable> result = new List<IListable>();

            foreach (var ingredient in _collection)
            {
                result.Add((IListable)ingredient);
            }

            return result;
        }

        public T Get(int id)
        {
            return _collection.FirstOrDefault(r => r.Id == id);
        }

        public void Create(T item)
        {
            _collection.Add(item);
        }

        public void Update(T item)
        {
            T existing = _collection.FirstOrDefault(r => r.Id == item.Id);

            if (existing != null)
            {
                int index = _collection.IndexOf(existing);
                _collection.Insert(index, item);
            }
        }

        public void Delete(int id)
        {
            T existing = _collection.FirstOrDefault(r => r.Id == id);

            if (existing != null)
            {
                int index = _collection.IndexOf(existing);
                _collection.RemoveAt(index);
            }
        }

    }

}