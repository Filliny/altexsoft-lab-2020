using System;
using Newtonsoft.Json;

namespace Recipes.Models
{

    //For classes showed in itemsView
    public interface IListable : IRelational, IComparable
    {

        string Name { get; set; }

        [JsonIgnore]
        bool Active { get; set; }

        [JsonIgnore]
        bool Selected { get; set; }

    }

}