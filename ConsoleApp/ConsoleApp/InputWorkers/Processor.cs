using ConsoleApp.FileHelpers;
using ConsoleApp.FileProcess;

namespace ConsoleApp.InputWorkers
{
    class Processor
    {
        public void Process(MyArgs args)
        {
            if (args.FilePath != null)
            {
                if (args.Remove)
                {
                    Remover textRemover = new Remover();
                    textRemover.RemoveWord(args);
                }

                if (args.ShowTen)
                {
                    Counter textCounter = new Counter();
                    textCounter.CountWords(args);
                }

                if (args.ReverseSentence)
                {
                    Reverser textReverser = new Reverser();
                    textReverser.ReverseSentence(args, 3);
                }
            }

            if (args.DirPath != null)
            {
                FileBrowser browser = new FileBrowser();
                browser.ShowContent(args.DirPath, args);
            }
        }
    }
}