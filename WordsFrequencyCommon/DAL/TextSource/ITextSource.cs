namespace WordsFrequency.Common.DAL
{
    public interface ITextSource
    {
        string ReadTextToBuffer();
        string GetBufferedText();
    }
}