namespace WordsFrequency.Common.DAL.Entities
{
    public interface IWordsCount
    {
        string Word { get; set;  }
        int Count { get; set; }
    }
}