using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordsFrequency.Common.DAL.Entities
{
    [Table("WordsCount")]
    public class WordsCountBase : EntityBase
    {
        public string Word { get; set; }
        public int Count { get; set; }

        public static WordsCountBase CreateInstance()
        {
            return CreateInstance<WordsCountBase>();
        }
    }
}
