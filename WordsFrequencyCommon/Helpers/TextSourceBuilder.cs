using WordsFrequency.Common.DAL;

namespace WordsFrequency.Common.Helpers
{
    public class TextSourceBuilder
    {
        public static ITextSource BuildConsoleSource()
        {
            return new ConsoleTextSource();
        }

        public static ITextSource BuildFileSource()
        {
            return new FileTextSource(new ConsoleFileHelper());
        }

        public static ITextSource BuildDbSource()
        {
            return new DbTextSource();
        }
    }
}