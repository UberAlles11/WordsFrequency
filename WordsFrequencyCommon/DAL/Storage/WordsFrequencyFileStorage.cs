using System;
using System.Collections.Generic;
using System.Text;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyFileStorage : IWordsFrequencyStorage
    {
        private IDictionary<string, int> wordsCount;

        public WordsFrequencyFileStorage(IDictionary<string, int> wordsCount)
        {
            Guard.Against<ArgumentNullException>(wordsCount == null, "WordsFrequencyFileRepository: wordsCount is null");
            this.wordsCount = wordsCount;
        }

        public void Commit()
        {
            var txt = new StringBuilder();
            wordsCount.ForEach(wc => txt.AppendFormat("{0} = {1}{2}", wc.Key, wc.Value, Environment.NewLine));

            string now = DateTime.Now.ToString().Replace(' ', '_').Replace('.', '-').Replace(':', '-');
            txt.TextToFileAsync(string.Format("WordsFrequency_{0}.txt",now));
        }
    }
}