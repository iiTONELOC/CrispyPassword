namespace Settings
{
    public class PasswordSettings
    {
        protected int _minLength = 8;
        protected int _maxLength = 128;

        protected bool _useLetters = false;
        protected bool _useNumbers = false;
        protected bool _useSymbols = false;
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
            set { _useLetters = value; }
        }
        public bool UseNumbers
        {
            get { return _useNumbers; }
            set { _useNumbers = value; }
        }
        public bool UseSymbols
        {
            get { return _useSymbols; }
            set { _useSymbols = value; }
        }
        public bool UseUppercase
        {
            get { return _useUppercase; }
            set { _useUppercase = value; }
        }
    }
}