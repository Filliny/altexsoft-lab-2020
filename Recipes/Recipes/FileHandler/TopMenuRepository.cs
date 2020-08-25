using Recipes.Controllers;
using Recipes.Models;
using System;
using System.Collections.Generic;

namespace Recipes.FileHandler
{

    class TopMenuRepository : IRepository<TopMenu>
    {

        private IStorageContext _storageContext;

        public TopMenuRepository(IStorageContext context)
        {
            _storageContext = context;
        }

        public IList<TopMenu> GetAll() //not sure if it be used
        {
            IList<TopMenu> result = new List<TopMenu>();  
            result.Add(_storageContext.TopMenu);

            return result;
        }

        public TopMenu Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(TopMenu item)
        {
            throw new NotImplementedException();
        }

        public void Update(TopMenu item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }

}