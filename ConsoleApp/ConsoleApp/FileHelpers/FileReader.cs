using System.IO;

namespace ConsoleApp.FileHelpers
{
    class FileReader
    {
        public string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(path);
        }
    }
}
