using System;
using System.IO;
using System.Text;
using WordsFrequency.Common.Extensions;
using WordsFrequency.Common.Text;

namespace WordsFrequency.Common.DAL
{
    public class ConsoleTextSource : ITextSource
    {
        ITextProvider provider;

        public ConsoleTextSource(ITextProvider provider)
        {
            Guard.Against<ArgumentNullException>(provider.IsNull(), "ConsoleTextSource: provider is null");

            this.provider = provider;
        }

        public string GetBufferedText()
        {
            return provider.Text;
        }

        public string ReadText()
        {
            Console.WriteLine(">> Введите текст (ввод пустой строки - закончить):");

            StringBuilder sb = new StringBuilder();
            string inputLine = String.Empty;

            using (StreamReader reader = new StreamReader(Console.OpenStandardInput(8192), Console.InputEncoding))
            {
                while ((inputLine = reader.ReadLine()) != null && !string.IsNullOrEmpty(inputLine))
                {
                    sb.AppendLine(inputLine);
                }
            }

            provider.Text = sb.ToString();
            return provider.Text;
        }
    }
}