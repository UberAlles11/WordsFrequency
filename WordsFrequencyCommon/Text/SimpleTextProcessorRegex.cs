using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.Text
{
    public class SimpleTextProcessorRegex : ITextProcessor
    {
        ITextProvider provider;

        public SimpleTextProcessorRegex(ITextProvider provider)
        {
            this.provider = provider;
        }

        public IEnumerable<string> GetWords()
        {
            if (provider.Text.IsNullOrEmpty())
                return new List<string>();

            var cleanedText = Regex.Replace(provider.Text, "[_«»\\(\\)<>\\[\\]\\*\\//]", " ", RegexOptions.IgnoreCase);
            return cleanedText.Split(new[] { ' ', '"', '.', ',', ';', ':', '!', '?', '+', '=', '-', '—', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => new Regex(@"\b\w+").IsMatch(s));
        }
    }
}