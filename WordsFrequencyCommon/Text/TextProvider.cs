namespace WordsFrequency.Common.Text
{
    public class TextProvider : ITextProvider
    {
        public string Text { get ; set ; }

        public TextProvider()
        {
            Text = string.Empty;
        }
    }
}
