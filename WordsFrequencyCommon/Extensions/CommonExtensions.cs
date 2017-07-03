using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordsFrequency.Common.Extensions
{
    public static class CommonExtensions
    {
        public static void TextToFile(this StringBuilder text, string path)
        {
            Guard.Against<ArgumentNullException>(path.IsNullOrEmpty(), "TextToFile: path is null");
            using (StreamWriter file = new StreamWriter(path))
            {
                file.Write(text.ToString());
            }
        }

        public static void TextToFile(this string text, string path)
        {
            Guard.Against<ArgumentNullException>(path.IsNullOrEmpty(), "TextToFile: path is null");
            using (StreamWriter file = new StreamWriter(path))
            {
                file.Write(text);
            }
        }

        public static async void TextToFileAsync(this StringBuilder text, string path)
        {
            Guard.Against<ArgumentNullException>(path.IsNullOrEmpty(), "TextToFileAsync: path is null");

            // Set a variable to the My Documents path.
            //string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the text asynchronously to a new file named "WriteTextAsync.txt".
            using (var outputFile = new StreamWriter(path))
            {
                await outputFile.WriteAsync(text.ToString());
            }
        }

        public static async void TextToFileAsync(this string text, string path)
        {
            Guard.Against<ArgumentNullException>(path.IsNullOrEmpty(), "TextToFileAsync: path is null");
            // Set a variable to the My Documents path.
            //string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the text asynchronously to a new file named "WriteTextAsync.txt".
            using (var outputFile = new StreamWriter(path))
            {
                await outputFile.WriteAsync(text.ToString());
            }
        }
    }
}