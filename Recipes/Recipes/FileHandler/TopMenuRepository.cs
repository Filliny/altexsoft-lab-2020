using Recipes.Models;

using System.Collections.Generic;

namespace Recipes.FileHandler
{

    class TopMenuRepository : Repository<TopCategory>
    {

        public TopMenuRepository(IList<TopCategory> collection)
            : base(collection)
        { }

    }

}