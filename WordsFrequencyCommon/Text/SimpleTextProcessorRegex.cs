using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.Text
{
    public class SimpleTextProcessorRegex : ITextProcessor
    {
        public IEnumerable<string> GetWords(string text)
        {
            if (text.IsNullOrEmpty())
                return new List<string>();

            var cleanedText = Regex.Replace(text, "[_«»\\(\\)<>\\[\\]\\*\\//]", " ", RegexOptions.IgnoreCase);
            return cleanedText.Split(new[] { ' ', '"', '.', ',', ';', ':', '!', '?', '+', '=', '-', '—', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => new Regex(@"\b\w+").IsMatch(s));
        }
    }
}