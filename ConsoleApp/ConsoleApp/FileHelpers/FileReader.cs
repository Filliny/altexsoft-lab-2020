using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.FileHelpers
{
    static class FileReader
    {
        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(path);
        }
    }
}
