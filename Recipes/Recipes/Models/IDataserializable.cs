using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using Recipes.DbHandler;

namespace Recipes.Models
{
    public interface IDataserializable 
    {
        string DbFilename { get; }
    } 

}
