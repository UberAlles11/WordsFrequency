using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsFrequency.Common.DAL;
using WordsFrequency.Common.Text;

namespace WordsFrequency
{
    public class WordsCounter// : IWordsCounter
    {
        private readonly IWordsFrequency _source;
        private readonly IWordsFrequencyPresenter _presenter;

        public WordsCounter(IWordsFrequency src, IWordsFrequencyPresenter presenter)
        {
            _source = src;
            _presenter = presenter;
        }

        public void Process()
        {
            Console.WriteLine("WordsCounter:Process()");
            _presenter.Commit();
            return;
        }
    }    
}
