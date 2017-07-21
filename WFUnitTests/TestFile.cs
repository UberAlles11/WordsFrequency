using System.IO;
using WordsFrequency.UI;

namespace WFUnitTests
{
    public class TestFile
    {
        public string FilePath { get; set; }

        public TestFile(string path, string text)
        {
            FilePath = path;

            Create(text);
        }

        public void Create(string text)
        {
            using (StreamWriter sw = File.CreateText(FilePath))
            {
                sw.WriteLine(text);
            }
        }

        public void Delete()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
