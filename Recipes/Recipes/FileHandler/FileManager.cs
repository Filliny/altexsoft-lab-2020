using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Recipes.Models;

namespace Recipes.FileHandler
{

    class FileManager:IFileManager 
    {

        public IList<T> ReadFile<T>()
        {
            string fileName = typeof(T).Name + ".json";

            IList<T> reqInstance = new List<T>();

            if (File.Exists(fileName))
            {
                string jsonIn = File.ReadAllText(fileName);
                reqInstance = JsonConvert.DeserializeObject<IList<T>>(jsonIn);
            }
            else
            {
                throw new FileNotFoundException("Db File missed !", fileName);
            }

            return reqInstance;
        }

        public void WriteFile<T>(IList<T> savingInstance)
        {
            string jsonOut = JsonConvert.SerializeObject(savingInstance, Formatting.Indented);

            File.WriteAllText(typeof(T).Name + ".json", jsonOut);

        }

    }

}