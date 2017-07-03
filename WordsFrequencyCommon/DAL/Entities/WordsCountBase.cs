using System.ComponentModel.DataAnnotations.Schema;

namespace WordsFrequency.Common.DAL.Entities
{
    [Table("WordsCount")]
    public class WordsCountBase : EntityBase, IWordsCount
    {
        public string Word { get; set; }
        public int Count { get; set; }        
    }
}
