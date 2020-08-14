using Newtonsoft.Json;
using Recipes.Models;
using System;
using System.IO;
using System.Linq;

namespace Recipes.DbHandler
{

    public interface IDbReader
    {

        IDataserializable ReadDb<T>();

    }

    class DbReader : IDbReader
    {

        public void GetDataTypes()
        {
            var type = typeof(IDataserializable);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var VARIABLE in types)
            {

                if (!VARIABLE.IsInterface)
                {
                    var TypeofInstance = Activator.CreateInstance(VARIABLE);
                }

            }
        }


        public IDataserializable ReadDb<T>()
        {
            var reqType = typeof(T);
            
            IDataserializable reqInstance =  (IDataserializable)Activator.CreateInstance(reqType);

            if (File.Exists(reqInstance.DbFilename))
            {
                string jsonIn = File.ReadAllText(reqInstance.DbFilename);
                reqInstance =(IDataserializable) JsonConvert.DeserializeObject<T>(jsonIn);
            }
            else
            {
                throw new FileNotFoundException("Db File missed !", reqInstance.DbFilename);
            }

            return reqInstance;
        }

    }

}