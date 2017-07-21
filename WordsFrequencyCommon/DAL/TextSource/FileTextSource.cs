using System;
using System.IO;
using WordsFrequency.Common.Extensions;
using WordsFrequency.Common.Text;
using WordsFrequency.UI;

namespace WordsFrequency.Common.DAL
{
    public class FileTextSource : ITextSource
    {
        private IFilePathProvider fileHelper;
        private ITextProvider provider;

        public FileTextSource(IFilePathProvider fileHelper, ITextProvider provider)
        {
            Guard.Against<ArgumentNullException>(fileHelper.IsNull(), "FileTextSource: fileHelper is null");
            Guard.Against<ArgumentNullException>(provider.IsNull(), "FileTextSource: provider is null");

            this.fileHelper = fileHelper;
            this.provider = provider;
        }

        public string GetBufferedText()
        {
            return provider.Text;
        }

        public string ReadText()
        {            
            string path = fileHelper.GetPath();

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var file = new FileInfo(path);
            
            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    provider.Text = sr.ReadToEnd();
                }
            }
            return provider.Text;
        }
    }
}