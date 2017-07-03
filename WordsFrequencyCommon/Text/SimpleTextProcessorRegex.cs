using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordsFrequency.Common.Text
{
    public class SimpleTextProcessorRegex : ITextProcessor
    {
        private string source;

        public SimpleTextProcessorRegex(string text)
        {
            this.source = text;
        }

        public IEnumerable<string> GetWords()
        {
            var text = Regex.Replace(source, "[_«»\\(\\)<>\\[\\]\\*\\//]", " ", RegexOptions.IgnoreCase);
            return text.Split(new[] { ' ', '"', '.', ',', ';', ':', '!', '?', '+', '=', '-', '—', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => new Regex(@"\b\w+").IsMatch(s));
        }
    }
}