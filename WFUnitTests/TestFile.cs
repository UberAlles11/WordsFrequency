using System.IO;
using WordsFrequency.UI;

namespace WFUnitTests
{
    public class TestFile
    {
        public string FilePath { get; }
        public string Text { get; private set; }

        public TestFile(string path, string text)
        {
            FilePath = path;

            Create(text);
        }

        public void Create(string text)
        {
            Text = text;

            using (StreamWriter sw = File.CreateText(FilePath))
            {                
                sw.Write(Text);
            }
        }

        public void Delete()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
