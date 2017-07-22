using System.Collections.Generic;
using System.Linq;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.Text
{
    public class WordsFrequencyProcessor : IWordsFrequencyProcessor
    {
        public IDictionary<string, int> GetWordsFrequency(IEnumerable<string> words)
        {
            if (words.IsNullOrEmpty())
                return new Dictionary<string, int>();

            return words.GroupBy(w => w)
                .Select(word => new
                {
                    Word = word.Key,
                    Frequency = word.Sum(w => 1)
                })
                //.Where(w => w.Frequency > 1)
                .OrderByDescending(w => w.Frequency)
                .ToDictionary(x => x.Word, x => x.Frequency);
        }
    }
}
