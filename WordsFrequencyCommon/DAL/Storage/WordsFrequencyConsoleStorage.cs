using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordsFrequency.Common.Extensions;
using WordsFrequency.UI;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyConsoleStorage : IWordsFrequencyStorage
    {
        private IDictionary<string, int> wordsCount;
        int lengthThreshold = 0;
        int frequencyThreshold = 0;
        IConsole console;

        public WordsFrequencyConsoleStorage(IDictionary<string, int> wordsCount, IConsole console, int lengthThreshold = 0, int frequencyThreshold = 0)
        {
            Guard.Against<ArgumentNullException>(wordsCount == null, "WordsFrequencyConsoleRepository: wordsCount is null");
            this.wordsCount = wordsCount;
            this.lengthThreshold = lengthThreshold;
            this.frequencyThreshold = frequencyThreshold;
            this.console = console;
        }

        public void Commit()
        {
            StringBuilder txt = new StringBuilder();
            wordsCount.Where(wc => wc.Key.Length > lengthThreshold && wc.Value > frequencyThreshold)
                .ForEach(wc => txt.AppendFormat("{0} = {1}{2}", wc.Key, wc.Value, Environment.NewLine));
            console.WriteLine(txt.ToString());
        }
    }
}