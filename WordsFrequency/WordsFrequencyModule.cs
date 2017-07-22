using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Text;
using WordsFrequency.UI;

namespace WordsFrequency
{
    public class WordsFrequencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleUI>().AsSelf();
            builder.RegisterType<ConsoleUI>().As<IConsole>();

            // SOURCE SECTION

            builder.RegisterType<SimpleTextProcessorRegex>().As<ITextProcessor>();

            builder.RegisterType<ConsoleTextSource>().Named<ITextSource>(SourceType.Console.ToString());

            builder.RegisterType<ConsoleFilePathProvider>().As<IFilePathProvider>();
            builder.RegisterType<FileTextSource>().Named<ITextSource>(SourceType.File.ToString());

            builder.RegisterType<DbTextSource>().Named<ITextSource>(SourceType.Database.ToString());

            builder.RegisterType<WordsFrequencyProcessor>().As<IWordsFrequencyProcessor>();

            // STORAGE SECTION
            
            builder.RegisterType<WordsFrequencyConsoleStorage>()
                .Named<IWordsFrequencyStorage>(SourceType.Console.ToString());
            builder.RegisterType<WordsFrequencyFileStorage>()
                .Named<IWordsFrequencyStorage>(SourceType.File.ToString());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<WordsFrequencyDbStorage>()
                .Named<IWordsFrequencyStorage>(SourceType.Database.ToString());
        }
    }
}
