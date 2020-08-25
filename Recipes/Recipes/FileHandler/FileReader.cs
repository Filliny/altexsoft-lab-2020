using System;
using System.IO;
using Newtonsoft.Json;
using Recipes.Models;

namespace Recipes.FileHandler
{

    public interface IFileReader
    {
        IDataserializable ReadFile<T>();
    }

    class FileReader : IFileReader
    {

        public IDataserializable ReadFile<T>()
        {
            var reqType = typeof(T);

            IDataserializable reqInstance = (IDataserializable) Activator.CreateInstance(reqType);

            if (File.Exists(reqInstance.JsonFileName))
            {
                string jsonIn = File.ReadAllText(reqInstance.JsonFileName);
                reqInstance = (IDataserializable) JsonConvert.DeserializeObject<T>(jsonIn);
            }
            else
            {
                throw new FileNotFoundException("Db File missed !", reqInstance.JsonFileName);
            }

            return reqInstance;
        }

    }

}