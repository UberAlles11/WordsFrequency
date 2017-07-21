using System;
using System.IO;
using System.Linq;

namespace WordsFrequency.UI
{
    public class ConsoleFilePathProvider : IFilePathProvider
    {
        public string GetPath()
        {            
            var dir = new System.IO.DirectoryInfo(Environment.CurrentDirectory);

            Console.WriteLine(string.Format("Список файлов в текущем каталоге (..\\{0}):", dir.Name));

            var allowedExtensions = new[] { ".fb2", ".txt", ".cs", ".config" };
            var files = 
                dir.EnumerateFiles("*.*")
                .Where(file => allowedExtensions.Any(file.Name.ToLower().EndsWith))
                .ToList();

            Console.WriteLine("");

            files.ForEach(f => Console.WriteLine(f.Name));

            Console.WriteLine("");

            Console.WriteLine("Введите полный путь к файлу либо имя файла в текущем каталоге:");

            string path = String.Empty;
            while ((path = Console.ReadLine()) != null && !string.IsNullOrEmpty(path))
            {
                var file = new FileInfo(path);
                if (file.Exists && file.Length > 0)
                    break;
                Console.WriteLine("Имя файла введено некорректно, повторите ввод заново:");
            }

            return path;
        }
    }
}