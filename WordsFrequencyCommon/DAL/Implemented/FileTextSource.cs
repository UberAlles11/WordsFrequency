using System;
using System.IO;
using WordsFrequency.Common.Helpers;

namespace WordsFrequency.Common.DAL
{
    internal class FileTextSource : ITextSource
    {
        private IFileHelper fileHelper;

        public FileTextSource(IFileHelper fileHelper)
        {
            Guard.Against<ArgumentNullException>(fileHelper == null, "FileTextSource: IFileHelper is null");
            this.fileHelper = fileHelper;
        }

        public string GetText()
        {            
            string path = fileHelper.GetFilePath();

            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var file = new FileInfo(path);
            string text = String.Empty;
            if (file.Exists)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    text = sr.ReadToEnd();
                }
            }
            return text;
        }
    }
}