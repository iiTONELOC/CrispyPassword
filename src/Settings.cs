namespace _Settings
{
    public class PasswordSettings
    {
        protected int _minLength = 16;
        protected int _maxLength = 128;

        protected static string numberSet = "0123456789";
        protected static string symbolSet = "!@#$%^&*()_+-=[]{}|;':,./<>?";
        protected static string characterSet = "abcdefghijklmnopqrstuvwxyz";

        protected bool _useLetters = true;
        protected bool _useNumbers = true;
        protected bool _useSymbols = true;
        protected bool _useUppercase = false;

        public int MinLength
        {
            get { return _minLength; }
        }
        public int MaxLength
        {
            get { return _maxLength; }
        }

        public bool UseLetters
        {
            get { return _useLetters; }
        }
        public bool UseNumbers
        {
            get { return _useNumbers; }
        }
        public bool UseSymbols
        {
            get { return _useSymbols; }
        }
        public bool UseUppercase
        {
            get { return _useUppercase; }
        }
    }
}