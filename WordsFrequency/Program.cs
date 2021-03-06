﻿using Autofac;
using System;
using System.Data.Entity;
using System.Linq;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Extensions;
using WordsFrequency.Common.Text;
using WordsFrequency.UI;

namespace WordsFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            DbInit();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new WordsFrequencyModule());            

            using (var container = builder.Build())
            {
                var ui = container.Resolve<ConsoleUI>();

                do
                {
                    var text = string.Empty;
                    
                    while (text.IsNullOrEmpty())
                    {
                        ui.ShowRootMenu();

                        var typ = ui.ReadSourceType(); // Выбрать источник из 3х вариантов

                        using (var scope = container.BeginLifetimeScope())
                        {
                            var textSource = scope.ResolveNamed<ITextSource>(typ.ToString()); // получить требуемый источник из контейнера

                            if (textSource.IsNull())
                            {
                                ui.ShowError(">> Ошибка источника.");
                            }
                            else
                            {
                                text = textSource.ReadTextToBuffer(); // Получить текст из источника                                
                                if (text.IsNullOrEmpty())
                                {
                                    ui.ShowError(">> Текст не содержит слов.");
                                }
                                else
                                {
                                    ui.WriteText(text);
                                }
                            }
                        }
                    }

                    ui.Continue(">> Нажмите любую клавишу для продолжения...");

                    // Сохранение

                    IWordsFrequencyStorage storage = null;

                    do
                    {
                        ui.ShowSaveMenu();

                        var typ = ui.ReadStorageType();

                        using (var scope = container.BeginLifetimeScope())
                        {
                            var words = scope.Resolve<ITextProcessor>().GetWords(text);                            
                            

                            if (words.IsNullOrEmpty())
                            {
                                ui.ShowErrorWithEscape(">> Текст не содержит слов.");
                            }
                            else
                            {
                                ui.WriteLine(string.Format(">> Найдено {0} слов.{1}", words.Count(), Environment.NewLine));

                                var wordsCount = scope.Resolve<IWordsFrequencyProcessor>().GetWordsFrequency(words);
                                if (typ == SourceType.Console)
                                {
                                    wordsCount = wordsCount.Where(wc => wc.Key.Length > 3 && wc.Value > 2).ToDictionary(wc => wc.Key, wc => wc.Value);
                                }

                                storage = scope.ResolveNamed<IWordsFrequencyStorage>(typ.ToString()); // Получить нужный стораж из контейнера

                                if (!storage.IsNull())
                                    storage.Commit(wordsCount);
                                ui.WriteLine(string.Format(">> Выполнено{0}", Environment.NewLine));
                            }
                        }
                    } while (storage.IsNull() && !ui.EscapePressed());

                    if (!ui.EscapePressed())
                    {
                        ui.WriteLine(">> Нажмите любую клавишу для повторного запуска");
                        ui.WriteLine(">> или ESC для выхода из приложения...");
                        ui.ReadKey(false);
                    }
                } while (!ui.EscapePressed());
            }
        }

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
    }
}
