using Newtonsoft.Json;
using Recipes.Models;
using System.IO;

namespace Recipes.DbHandler
{

    internal interface IDbWriter
    {

        void WriteDbFile(IDataserializable savingInstance);

    }

    class DbWriter : IDbWriter
    {

        public void WriteDbFile(IDataserializable savingInstance)
        {
            string jsonOut = JsonConvert.SerializeObject(savingInstance, Formatting.Indented,
                new JsonSerializerSettings {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            File.WriteAllText(savingInstance.DbFilename, jsonOut);

        }

    }

}