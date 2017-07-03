using System;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            AppHelper.DbInit();

            var keyPressed = new ConsoleKeyInfo();
            do
            {
                var text = string.Empty;

                while (text.IsNullOrEmpty())
                {
                    AppHelper.ShowRootMenu();

                    var textSource = AppHelper.SelectSource(); // Выбрать источник из 3х вариантов

                    if (textSource == null)
                    {
                        Console.WriteLine(">> Ошибка ввода.");
                        Console.WriteLine(">> Нажмите любую клавишу и сделайте выбор заново.");
                        Console.ReadKey(false);
                    }
                    else
                    {
                        text = textSource.GetText(); // Получить текст из источника
                        if (text.IsNullOrEmpty())
                        {
                            Console.WriteLine(">> Текст не содержит слов.");
                            Console.WriteLine(">> Нажмите любую клавишу и сделайте выбор заново.");
                            Console.ReadKey(false);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(text);
                            Console.WriteLine();
                        }
                    }
                }

                Console.WriteLine(">> Нажмите любую клавишу для продолжения...");
                Console.ReadKey(false);

                var wordsCount = AppHelper.GetWordsFrequency(text); // Посчитать частоту слов

                // Сохранение

                IWordsFrequencyRepository saveRepository = null;
                
                do
                {
                    AppHelper.ShowSaveMenu();

                    saveRepository = AppHelper.SelectSaveRepository(wordsCount); // Выбрать источник из 3х вариантов

                    if (saveRepository == null)
                    {
                        Console.WriteLine(">> Ошибка ввода.");
                        Console.WriteLine(">> Нажмите любую клавишу и сделайте выбор заново.");
                        Console.WriteLine(">> Либо нажмите ESC для отмены.");
                        keyPressed = Console.ReadKey(false);
                    }
                    else
                    {
                        saveRepository.Commit();
                        Console.WriteLine(">> Выполнено");
                        Console.WriteLine();
                    }
                } while (saveRepository == null && keyPressed.Key != ConsoleKey.Escape);


                if (keyPressed.Key != ConsoleKey.Escape)
                {                    
                    Console.WriteLine(">> Нажмите любую клавишу для повторного запуска");
                    Console.WriteLine(">> или ESC для выхода из приложения...");
                    keyPressed = Console.ReadKey(false);
                }
                else
                {
                    keyPressed = new ConsoleKeyInfo();
                }

            } while (keyPressed.Key != ConsoleKey.Escape);            
        }        
    }
}
