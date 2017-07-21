using System.Collections.Generic;
using System.Linq;

namespace WordsFrequency.Common.Text
{
    public class WordsFrequencyProcessor : IWordsFrequencyProcessor
    {
        ITextProcessor processor;

        public WordsFrequencyProcessor(ITextProcessor processor)
        {
            this.processor = processor;
        }

        public IDictionary<string, int> GetWordsFrequency()
        {
            return processor.GetWords()
                .GroupBy(w => w)
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
