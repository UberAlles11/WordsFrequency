using System;
using System.Collections.Generic;

namespace WordsFrequency.Common.Text
{
    public class SimpleTextProcessor : ITextProcessor
    {
        private string source;

        public SimpleTextProcessor(string text)
        {
            this.source = text;
        }

        public IEnumerable<string> GetWords()
        {
            return source.Split(new[] { ' ', '"', '.', ',', ';', ':', '!', '?', '+', '=', '-', '—', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);                
        }
    }
}