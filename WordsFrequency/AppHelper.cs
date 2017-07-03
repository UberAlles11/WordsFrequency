using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WordsFrequency.Common;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Extensions;
using WordsFrequency.Common.Helpers;
using WordsFrequency.Common.Text;

namespace WordsFrequency
{
    internal class AppHelper
    {
        /// <summary>
        ///     The perform database operations.
        /// </summary>
        internal static void DbInit()
        {
            Database.SetInitializer(new DataInitializer());
            using (var db = new DataContext())
            {
                db.Database.Initialize(true);
            }
        }

        internal static void ShowRootMenu()
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

        internal static ITextSource SelectSource()
        {
            if (int.TryParse(Console.ReadLine(), out int iDecision))
            {
                switch (iDecision)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine(">> Введите текст (ввод пустой строки - закончить):");
                        return TextSourceBuilder.BuildConsoleSource();
                    case 2:
                        Console.WriteLine(">> Загрузка файла...");
                        return TextSourceBuilder.BuildFileSource();
                    case 3:
                        Console.WriteLine(">> Загрузка текста из БД...");
                        return TextSourceBuilder.BuildDbSource();
                    default:
                        break;
                }
            }

            return null;
        }

        internal static IDictionary<string, int> GetWordsFrequency(string text)
        {
            var wordsCount = new Dictionary<string, int>();
            if (text.IsNullOrEmpty())
                return wordsCount;

            ITextProcessor processor = new SimpleTextProcessorRegex(text.ToLower());
            foreach (var word in processor.GetWords())
            {
                if (wordsCount.ContainsKey(word))
                    wordsCount[word]++;
                else
                    wordsCount[word] = 1;
            }
            return wordsCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        internal static void ShowSaveMenu()
        {
            Console.Clear();
            Console.WriteLine(">> Выберите вариант сохранения (вывода) результата:");
            Console.WriteLine("1. Файл");
            Console.WriteLine("2. База данных");
            Console.WriteLine("3. Экран");
        }

        internal static IWordsFrequencyRepository SelectSaveRepository(IDictionary<string, int> wordsCount)
        {
            Guard.Against<ArgumentNullException>(wordsCount == null, "SelectSaveRepository: wordsCount is null");

            if (int.TryParse(Console.ReadLine(), out int iDecision))
            {
                switch (iDecision)
                {
                    case 1:
                        Console.WriteLine(string.Format(">> Сохранение в файл {0} записей...", wordsCount.Count));
                        return new WordsFrequencyFileRepository(wordsCount);
                    case 2:
                        Console.WriteLine(string.Format(">> Сохранение в БД {0} записей...", wordsCount.Count));
                        return new WordsFrequencyDbRepository(wordsCount);
                    case 3:
                        Console.WriteLine(string.Format(">> Вывод слов с наибольшей частотой и длинной более 4:", wordsCount.Count));
                        return new WordsFrequencyConsoleRepository(wordsCount);
                    default:
                        break;
                }
            }

            return null;
        }
    }
}
