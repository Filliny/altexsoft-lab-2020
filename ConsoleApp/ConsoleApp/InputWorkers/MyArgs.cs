using PowerArgs;

namespace ConsoleApp.InputWorkers
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class MyArgs
    {
        [HelpHook, ArgShortcut("-h"), ArgDescription("Shows this help")]
        public bool Help { get; set; }

        [ArgExistingFile, ArgDescription("Path to file to process")]
        public string FilePath { get; set; }

        [ArgExistingDirectory, ArgDescription("Path to folder to browse")]
        public string DirPath { get; set; }

        [ArgShortcut("-R"), ArgDescription("Remove any word or char in all text. Use with -W")]
        public bool Remove { get; set; }

        [ArgShortcut("-W"), ArgDescription("Word to remove e.g. -W word_to_remove")]
        public string Word { get; set; }

        [ArgShortcut("-S"), ArgDescription("Show each ten word in text")]
        public bool ShowTen { get; set; }

        [ArgShortcut("-T"), ArgDescription("Show reversed sentence in text. Use with number of sentence -N")]
        public bool ReverseSentence { get; set; }

    }
}