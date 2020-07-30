using PowerArgs;

namespace ConsoleApp.Instant
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    class MyArgs
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

        [ArgShortcut("-T"), ArgDescription("Show third reversed sentence in text")]
        public bool ThirdSentence { get; set; }
    }
}
