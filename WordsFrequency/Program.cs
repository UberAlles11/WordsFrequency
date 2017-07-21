using Autofac;
using System;
using System.Data.Entity;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Extensions;
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
                                text = textSource.ReadText(); // Получить текст из источника                                
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
                            storage = scope.ResolveNamed<IWordsFrequencyStorage>(typ.ToString()); // Получить нужный стораж из контейнера

                            if (storage.IsNull())
                            {
                                ui.ShowErrorWithEscape(">> Текст не содержит слов.");
                            }
                            else
                            {
                                storage.Commit();
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
