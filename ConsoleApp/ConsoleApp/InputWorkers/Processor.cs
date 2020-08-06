using ConsoleApp.FileHelpers;
using ConsoleApp.FileProcess;

namespace ConsoleApp.InputWorkers
{
    class Processor
    {
        public void Process(MyArgs args, ICounter counter, IRemover remover, 
            IReverser reverser, IFileBrowser browser) //Method injection
        {
            while (true)
            {
                if (args.FilePath != null)
                {
                    if (args.Remove)
                    {
                        remover.RemoveWord(args);
                    }

                    if (args.ShowTen)
                    {
                        counter.CountWords(args);
                    }

                    if (args.ReverseSentence)
                    {
                        reverser.ReverseSentence(args, 3);
                    }
                }

                if (args.DirPath != null)
                {
                    if (browser.ShowContent(args.DirPath, args))
                    {
                        continue;
                    }
                }

                break;
            }
        }
    }
}