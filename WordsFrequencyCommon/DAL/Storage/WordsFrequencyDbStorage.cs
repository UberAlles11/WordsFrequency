using System;
using System.Collections.Generic;
using WordsFrequency.Common.DAL.Entities;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyDbStorage : IWordsFrequencyStorage
    {
        private IDictionary<string, int> wordsCount;
        IUnitOfWork uow;

        public WordsFrequencyDbStorage(IDictionary<string, int> wordsCount, IUnitOfWork uow)
        {
            Guard.Against<ArgumentNullException>(wordsCount == null, "WordsFrequencyDbRepository: wordsCount is null");
            Guard.Against<ArgumentNullException>(uow == null, "WordsFrequencyDbRepository: wordsCount is null");
            this.wordsCount = wordsCount;
            this.uow = uow;
        }

        public void Commit()
        {
            uow.RemoveAll<WordsCountBase>();
            wordsCount.ForEach(wc =>
            {
                var entity = WordsCountBase.CreateInstance();
                entity.Word = wc.Key;
                entity.Count = wc.Value;
                uow.Add(entity);
            });
            uow.SaveChanges();            
        }        
    }
}