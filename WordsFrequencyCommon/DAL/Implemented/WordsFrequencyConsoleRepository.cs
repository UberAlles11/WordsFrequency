using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyConsoleRepository : IWordsFrequencyRepository
    {
        private IDictionary<string, int> wordsCount;

        public WordsFrequencyConsoleRepository(IDictionary<string, int> wordsCount)
        {
            Guard.Against<ArgumentNullException>(wordsCount == null, "WordsFrequencyConsoleRepository: wordsCount is null");
            this.wordsCount = wordsCount;
        }

        public void Commit()
        {
            StringBuilder txt = new StringBuilder();
            wordsCount.Where(wc => wc.Key.Length > 4 && wc.Value > 2)
                .ForEach(wc => txt.AppendFormat("{0} = {1}{2}", wc.Key, wc.Value, Environment.NewLine));
            Console.WriteLine(txt);
        }
    }
}