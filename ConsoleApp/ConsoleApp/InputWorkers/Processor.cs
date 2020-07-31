using ConsoleApp.FileHelpers;
using ConsoleApp.FileProcess;
using ConsoleApp.InputWorkers;

namespace ConsoleApp.InputWorkers
{
    static class Processor
    {
        public static void Process(MyArgs args)
        {
            if (args.FilePath != null)
            {
                if (args.Remove)
                {
                    Remover.Remove(args);
                }

                if (args.ShowTen)
                {
                    Counter.CountWords(args);
                }

                if (args.ThirdSentence)
                {
                    Reverser.ReverseSentence(args, 2);
                }
            }

            if (args.DirPath != null)
            {
                FileBrowser.ShowContent(args.DirPath, args);
            }
        }
    }
}