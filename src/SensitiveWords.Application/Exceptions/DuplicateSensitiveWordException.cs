namespace SensitiveWords.Application.Exceptions
{
    public class DuplicateSensitiveWordException : Exception
    {
        public DuplicateSensitiveWordException(string word)
            : base($"Sensitive word '{word}' already exists.")
        {
        }
    }
}
