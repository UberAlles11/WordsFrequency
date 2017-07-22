using System;
using System.IO;
using System.Text;

namespace WordsFrequency.Common.DAL
{
    public class ConsoleTextSource : ITextSource
    {
        string _textBuffer = string.Empty;


        public string GetBufferedText()
        {
            return _textBuffer;
        }

        public string ReadTextToBuffer()
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

            _textBuffer = sb.ToString();
            return _textBuffer;
        }
    }
}