using System;
using System.IO;
using System.Text;

namespace WordsFrequency.Common.DAL
{
    public class ConsoleTextSource : ITextSource
    {
        public string GetText()
        {
            string source = String.Empty;
            StringBuilder sb = new StringBuilder();
            string inputLine = String.Empty;

            using (StreamReader reader = new StreamReader(Console.OpenStandardInput(8192), Console.InputEncoding))
            {
                while ((inputLine = reader.ReadLine()) != null && !string.IsNullOrEmpty(inputLine))
                {
                    sb.AppendLine(inputLine);
                }
            }
            return sb.ToString();
        }
    }
}