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
        string _textBuffer;

        public FileTextSource(IFilePathProvider fileHelper)//, ITextProvider provider)
        {
            Guard.Against<ArgumentNullException>(fileHelper.IsNull(), "FileTextSource: fileHelper is null");

            this.fileHelper = fileHelper;
        }

        public string GetBufferedText()
        {
            return _textBuffer;
        }

        public string ReadTextToBuffer()
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
                    _textBuffer = sr.ReadToEnd();
                }
            }
            return _textBuffer;
        }
    }
}