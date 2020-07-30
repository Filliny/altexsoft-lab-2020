using System;
using System.IO;

namespace ConsoleApp.Instant
{
    static class PathValidator //left from earlier validator to help with user input
    {
        public static bool PathTryParse(string input, ref string path)
        {
            if (!Path.IsPathRooted(input))
            {
                input = "\\" + input;
            }

            if (!Path.IsPathFullyQualified(input))
            {
                input = Environment.CurrentDirectory + input;
            }

            if (File.Exists(input) || Directory.Exists(input))
            {
                path = input;
                return true;
            }

            return false;
        }
    }
}
