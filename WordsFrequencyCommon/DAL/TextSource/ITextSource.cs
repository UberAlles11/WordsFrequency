namespace WordsFrequency.Common.DAL
{
    public interface ITextSource
    {
        string ReadText();
        string GetBufferedText();
    }
}