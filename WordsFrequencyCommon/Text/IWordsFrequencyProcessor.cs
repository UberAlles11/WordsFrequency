using System.Collections.Generic;

namespace WordsFrequency.Common.Text
{
    public interface IWordsFrequencyProcessor
    {
        IDictionary<string, int> GetWordsFrequency();
    }
}
