using System;
using System.Linq;
using WordsFrequency.Common.DAL.Entities;

namespace WordsFrequency.Common.DAL
{
    public class DbTextSource : ITextSource
    {
        public string GetText()
        {
            var text = String.Empty;
            using (var data = new DbDataRepository<SourceTextBase>())
            {
                text = data.GetAll().FirstOrDefault().Text;
            }
            return text;
        }
    }
}