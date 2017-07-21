using Autofac;
using Autofac.Core;
using System.Collections.Generic;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Text;
using WordsFrequency.UI;

namespace WordsFrequency
{
    public class WordsFrequencyModule : Module
    {
        //public bool ObeySpeedLimit { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleUI>().AsSelf();

            // SOURCE SECTION

            builder.RegisterType<TextProvider>().As<ITextProvider>().SingleInstance();

            builder.RegisterType<SimpleTextProcessorRegex>().As<ITextProcessor>();

            builder.RegisterType<ConsoleTextSource>().Named<ITextSource>(SourceType.Console.ToString());

            builder.RegisterType<ConsoleFilePathProvider>().As<IFilePathProvider>();
            builder.RegisterType<FileTextSource>().Named<ITextSource>(SourceType.File.ToString());

            builder.RegisterType<DbTextSource>().Named<ITextSource>(SourceType.Database.ToString());

            builder.RegisterType<WordsFrequencyProcessor>().As<IWordsFrequencyProcessor>();

            // PRESENTER SECTION

            builder.RegisterType<ConsoleUI>().As<IConsole>();
            builder.RegisterType<WordsFrequencyConsoleStorage>().Named<IWordsFrequencyStorage>(SourceType.Console.ToString())
                .WithParameter(
                new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IDictionary<string, int>) && pi.Name == "wordsCount",
                    (pi, ctx) => ctx.Resolve<IWordsFrequencyProcessor>().GetWordsFrequency())
                )
                .WithParameter("lengthThreshold", 3)
                .WithParameter("frequencyThreshold", 2);

            //Console.WriteLine(">> Загрузка файла...");                    
            builder.RegisterType<WordsFrequencyFileStorage>().Named<IWordsFrequencyStorage>(SourceType.File.ToString())
            .WithParameter(
                new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IDictionary<string, int>) && pi.Name == "wordsCount",
                    (pi, ctx) => ctx.Resolve<IWordsFrequencyProcessor>().GetWordsFrequency())
                );

            //Console.WriteLine(">> Загрузка текста из БД...");
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<WordsFrequencyDbStorage>().Named<IWordsFrequencyStorage>(SourceType.Database.ToString())
                .WithParameter(
                new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(IDictionary<string, int>) && pi.Name == "wordsCount",
                    (pi, ctx) => ctx.Resolve<IWordsFrequencyProcessor>().GetWordsFrequency())
                );
        }
    }
}
