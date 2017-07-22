using System.Collections.Generic;

namespace WordsFrequency.Common.DAL
{
    public interface IWordsFrequencyStorage
    {
        void Commit(IDictionary<string, int> wordsCount);
    }
}