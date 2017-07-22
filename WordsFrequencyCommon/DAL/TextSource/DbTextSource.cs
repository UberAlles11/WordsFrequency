using System;
using System.Linq;
using WordsFrequency.Common.DAL.Entities;
using WordsFrequency.Common.Extensions;
using WordsFrequency.Common.Text;

namespace WordsFrequency.Common.DAL
{
    public class DbTextSource : ITextSource
    {
        string _textBuffer = string.Empty;
        IUnitOfWork _uow;

        public DbTextSource(IUnitOfWork uow)
        {
            Guard.Against<ArgumentNullException>(uow.IsNull(), "DbTextSource: uow is null");            
            _uow = uow;
        }

        public string GetBufferedText()
        {
            return _textBuffer;
        }

        public string ReadTextToBuffer()
        {
            _textBuffer = _uow.All<SourceTextBase>().FirstOrDefault().Text;
            return _textBuffer;
        }
    }
}