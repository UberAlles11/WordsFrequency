using System;

namespace WordsFrequency.UI
{
    public enum SourceType { Exit = 0, Console = 1, File = 2, Database = 3, Invalid = 4 }

    public class ConsoleUI : IConsole
    {
        public ConsoleKeyInfo LastKeyPressed { get; set; }

        public void ShowRootMenu()
        {
            Console.Clear();
            Console.WriteLine("*** Демонстрационное приложение подсчета количества слова в тексте ***");
            Console.WriteLine();
            Console.WriteLine(">> Выберите вариант загрузки данных для обработки:");
            Console.WriteLine("1. Ручной ввод текста");
            Console.WriteLine("2. Загрузка текста из файла");
            Console.WriteLine("3. Загрузка демонстрационного текста из БД");
            Console.WriteLine("0. Выход из приложения");
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void ShowSaveMenu()
        {
            Console.Clear();
            Console.WriteLine(">> Выберите вариант сохранения (вывода) результата:");
            Console.WriteLine("1. Экран");
            Console.WriteLine("2. Файл");
            Console.WriteLine("3. База данных");            
        }

        public void ShowError(string message)
        {
            Console.WriteLine(message);
            Continue(">> Нажмите любую клавишу и сделайте выбор заново.");
        }

        public SourceType ReadSourceType()
        {
            int iDecision = -1;
            while (iDecision < 0)
            {
                var choice = Console.ReadLine();
                if (int.TryParse(choice, out iDecision))
                {
                    if (iDecision == 0)
                        Environment.Exit(0);

                    if (iDecision < 1 && iDecision > 3)
                    {
                        iDecision = -1;
                        continue;
                    }
                }
            }

            return (SourceType)iDecision;
        }

        public int WriteLine(string text)
        {
            Console.WriteLine(text);
            return 0;
        }

        public int WriteText(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
            Console.WriteLine();
            return 0;
        }

        public void Continue(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey(false);
        }

        public void ShowErrorWithEscape(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(">> Нажмите любую клавишу и сделайте выбор заново.");
            Console.WriteLine(">> Либо нажмите ESC для отмены.");
            ReadKey();
            return; 
        }

        public void ReadKey(bool showKey = false)
        {
            LastKeyPressed = Console.ReadKey(showKey);
        }

        public SourceType ReadStorageType()
        {
            int iDecision = -1;
            while (iDecision < 0)
            {
                var choice = Console.ReadLine();
                if (int.TryParse(choice, out iDecision))
                {
                    if (iDecision < 1 && iDecision > 3)
                    {
                        iDecision = -1;
                        continue;
                    }
                }
            }

            return (SourceType)iDecision;
        }

        public bool EscapePressed()
        {
            return LastKeyPressed.Key == ConsoleKey.Escape;
        }
    }
}
