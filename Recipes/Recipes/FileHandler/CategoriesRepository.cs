using System;
using System.Collections.Generic;
using System.Text;

using Recipes.Controllers;
using Recipes.Models;

namespace Recipes.FileHandler
{
    class CategoriesRepository: IRepository<Category>
    {
        private IStorageContext _storageContext;

        public CategoriesRepository(IStorageContext context)
        {
            _storageContext = context;
        }

        public IList<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Category item)
        {
            throw new NotImplementedException();
        }

        public void Update(Category item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
