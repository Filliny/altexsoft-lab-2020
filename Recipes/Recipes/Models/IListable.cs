using System;

using Newtonsoft.Json;

namespace Recipes.Models
{
    //For classes showed in itemsView
    public interface IListable: IComparable
    {

        int Id { get; set; }
        string Name { get; set; }

        [JsonIgnore]
        bool Active { get; set; }

    }

}