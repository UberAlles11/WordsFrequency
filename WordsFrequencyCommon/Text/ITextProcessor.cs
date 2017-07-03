using System.Collections.Generic;

namespace WordsFrequency.Common.Text
{
    public interface ITextProcessor
    {
        IEnumerable<string> GetWords();
    }
}