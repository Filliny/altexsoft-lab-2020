using System.IO;

namespace ConsoleApp.FileHelpers
{
    class FileReader
    {
        protected string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(path);
        }
    }
}
