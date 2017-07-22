using System;
using System.Collections.Generic;
using WordsFrequency.Common.DAL.Entities;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyDbStorage : IWordsFrequencyStorage
    {
        IUnitOfWork _uow;

        public WordsFrequencyDbStorage(IUnitOfWork uow)
        {
            Guard.Against<ArgumentNullException>(uow == null, "WordsFrequencyDbStorage: wordsCount is null");
            
            _uow = uow;
        }

        public void Commit(IDictionary<string, int> wordsCount)
        {
            Guard.Against<ArgumentNullException>(wordsCount.IsNullOrEmpty, "WordsFrequencyDbStorage: wordsCount is null");

            _uow.RemoveAll<WordsCountBase>();
            wordsCount.ForEach(wc =>
            {
                var entity = WordsCountBase.CreateInstance();
                entity.Word = wc.Key;
                entity.Count = wc.Value;
                _uow.Add(entity);
            });
            _uow.SaveChanges();            
        }        
    }
}