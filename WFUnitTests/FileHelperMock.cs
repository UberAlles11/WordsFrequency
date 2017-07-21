using System.IO;
using WordsFrequency.UI;

namespace WFUnitTests
{
    public class FileHelperMock : IFilePathProvider
    {
        string _path = string.Empty;

        public FileHelperMock()
        {
            _path = "FileHelperMockTestFile.txt";

            using (StreamWriter sw = File.CreateText(_path))
            {
                sw.WriteLine("FileHelper Mock Test");
            }
        }

        public string GetPath()
        {
            return _path;
        }
    }
}
