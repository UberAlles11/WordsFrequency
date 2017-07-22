using System;
using System.IO;
using System.Linq;

namespace WordsFrequency.UI
{
    public class ConsoleFilePathProvider : IFilePathProvider
    {
        IConsole _console;

        public ConsoleFilePathProvider(IConsole console)
        {
            _console = console;
        }

        public string GetPath()
        {            
            var dir = new DirectoryInfo(Environment.CurrentDirectory);

            _console.WriteLine(string.Format("Список файлов в текущем каталоге (..\\{0}):", dir.Name));

            var allowedExtensions = new[] { ".fb2", ".txt", ".cs", ".config" };
            var files = 
                dir.EnumerateFiles("*.*")
                .Where(file => allowedExtensions.Any(file.Name.ToLower().EndsWith))
                .ToList();

            _console.WriteLine("");

            files.ForEach(f => _console.WriteLine(f.Name));

            _console.WriteLine("");

            _console.WriteLine("Введите полный путь к файлу либо имя файла в текущем каталоге:");

            string path = String.Empty;
            while ((path = _console.ReadLine()) != null && !string.IsNullOrEmpty(path))
            {
                var file = new FileInfo(path);
                if (file.Exists && file.Length > 0)
                    break;
                _console.WriteLine("Имя файла введено некорректно, повторите ввод заново:");
            }

            return path;
        }
    }
}