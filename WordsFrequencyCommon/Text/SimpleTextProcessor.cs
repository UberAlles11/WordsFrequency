using System;
using System.Collections.Generic;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.Text
{
    public class SimpleTextProcessor : ITextProcessor
    {
        public IEnumerable<string> GetWords(string text)
        {
            if (text.IsNullOrEmpty())
                return new List<string>();

            return text.Split(new[] { ' ', '"', '.', ',', ';', ':', '!', '?', '+', '=', '-', '—', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}