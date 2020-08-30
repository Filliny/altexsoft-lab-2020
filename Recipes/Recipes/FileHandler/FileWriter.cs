using System.IO;
using Newtonsoft.Json;
using Recipes.Models;

namespace Recipes.FileHandler
{

    internal interface IFileWriter
    {

        void WriteFile(IDataserializable savingInstance);

    }

    class FileWriter : IFileWriter
    {

        public void WriteFile(IDataserializable savingInstance)
        {
            string jsonOut = JsonConvert.SerializeObject(savingInstance, Formatting.Indented,
                new JsonSerializerSettings {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            File.WriteAllText(savingInstance.JsonFileName, jsonOut);

        }

    }

}