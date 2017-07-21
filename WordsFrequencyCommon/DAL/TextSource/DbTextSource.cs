using System;
using System.Linq;
using WordsFrequency.Common.DAL.Entities;
using WordsFrequency.Common.Extensions;
using WordsFrequency.Common.Text;

namespace WordsFrequency.Common.DAL
{
    public class DbTextSource : ITextSource
    {
        ITextProvider provider;
        string textBuffer = string.Empty;
        IUnitOfWork uow;

        public DbTextSource(ITextProvider provider, IUnitOfWork uow)
        {
            Guard.Against<ArgumentNullException>(provider.IsNull(), "DbTextSource: provider is null");

            this.provider = provider;
            this.uow = uow;
        }

        public string GetBufferedText()
        {
            return provider.Text;
        }

        public string ReadText()
        {
            provider.Text = uow.All<SourceTextBase>().FirstOrDefault().Text;
            return provider.Text;
        }
    }
}