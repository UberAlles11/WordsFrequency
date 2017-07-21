using System;
using System.Collections.Generic;

namespace WordsFrequency.Common.Text
{
    public class SimpleTextProcessor : ITextProcessor
    {
        ITextProvider _provider;

        public SimpleTextProcessor(ITextProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<string> GetWords()
        {
            return _provider.Text.Split(new[] { ' ', '"', '.', ',', ';', ':', '!', '?', '+', '=', '-', '—', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);                
        }        
    }
}