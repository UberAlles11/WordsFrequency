using System;
using System.Collections.Generic;
using WordsFrequency.Common.DAL.Entities;
using WordsFrequency.Common.Extensions;

namespace WordsFrequency.Common.DAL
{
    public class WordsFrequencyDbRepository : IWordsFrequencyRepository
    {
        private IDictionary<string, int> wordsCount;

        public WordsFrequencyDbRepository(IDictionary<string, int> wordsCount)
        {
            Guard.Against<ArgumentNullException>(wordsCount == null, "WordsFrequencyDbRepository: wordsCount is null");
            this.wordsCount = wordsCount;
        }

        public void Commit()
        {
            using (var data = new DbDataRepository<WordsCountBase>())
            {
                DateTime t1 = DateTime.Now;
                /*
                var words = data.GetAll();

                var txt = new StringBuilder();
                words.ForEach(wc => txt.AppendFormat("{0} = {1}{2}", wc.Word, wc.Count, Environment.NewLine));

                string now = DateTime.Now.ToString().Replace(' ', '_').Replace('.', '-').Replace(':', '-');
                txt.TextToFileAsync(string.Format("WordsDB_{0}.txt", now));
                */
                //data.Delete(data.GetAll());
                data.DeleteAll();
                //data.Save();

                //int c = data.Count();

                DateTime t2 = DateTime.Now;
                wordsCount.ForEach(wc => data.Add(new WordsCountBase() { Word = wc.Key, Count = wc.Value }));
                data.Save();
                DateTime t3 = DateTime.Now;
                Console.WriteLine(string.Format("delete = {0} , commit = {1}",
                t2.Subtract(t1), t3.Subtract(t2)));
            }
        }        
    }
}