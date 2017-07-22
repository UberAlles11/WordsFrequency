using System;
using System.Collections.Generic;
using System.Text;
using WordsFrequency.Common.Extensions;
using WordsFrequency.UI;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyConsoleStorage : IWordsFrequencyStorage
    {
        IConsole _console;

        public WordsFrequencyConsoleStorage(IConsole console)
        {
            Guard.Against<ArgumentNullException>(console == null, "WordsFrequencyConsoleStorage: console is null");
            _console = console;
        }

        public void Commit(IDictionary<string, int> wordsCount)
        {
            Guard.Against<ArgumentNullException>(wordsCount.IsNull(), "WordsFrequencyConsoleStorage: wordsCount is null");

            StringBuilder txt = new StringBuilder();
            wordsCount.ForEach(wc => txt.AppendFormat("{0} = {1}{2}", wc.Key, wc.Value, Environment.NewLine));
            _console.WriteLine(txt.ToString());
        }
    }
}