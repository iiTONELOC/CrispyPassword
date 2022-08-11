using Utils;
using Settings;

namespace Generator

{
    public class _Generator : PasswordSettings
    {
        private string password = "";
        private string passwordChars = "";
        private static string numberSet = "0123456789";
        private static string symbolSet = "!@#$%^&*()_+-=[]{}|;':,./<>?";
        private static string characterSet = "abcdefghijklmnopqrstuvwxyz";

        private int _passwordLength;

        public _Generator()
        {
            password = "";
            passwordChars = "";
            _passwordLength = -1;
        }

        public int PasswordLength
        {
            get { return _passwordLength; }
            set { _passwordLength = value; }
        }

        /// <summary>
        /// Checks to see if at least one character type is selected.
        /// </summary>
        /// <returns>true if a password can be created, false otherwise</returns>
        private bool CanGenerate()
        {
            // MAKE SURE WE CAN CREATE A PASSWORD WITH THE PROVIDED OPTIONS
            if (!UseLetters && !UseNumbers && !UseSymbols && !UseUppercase)
            {
                ErrorMessages.Print(ErrorMessages.SelectionRequired);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Prompts and validates user input for Password Length, and determines if the user
        /// wants to use letters, uppercase, numbers, and/or symbols in their new password.
        /// </summary>
        private void PromptUserForPasswordRequirements()
        {
            PasswordLength = Input.PromptPasswordLength();
            UseLetters = Input.PromptLetters();
            UseUppercase = Input.PromptUpperCase();
            UseNumbers = Input.PromptNumbers();
            UseSymbols = Input.PromptSymbols();
        }
        private void GreetUser()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Welcome to the Password Generator!");
            Console.WriteLine("===================================");
            Console.WriteLine();
        }

        private string GeneratePassword()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, characterSet.Length);
            if (UseLetters)
            {
                passwordChars += characterSet;
                password += characterSet[rnd.Next(randomNumber, characterSet.Length)];
            }
            if (UseNumbers)
            {
                passwordChars += numberSet;
                randomNumber = rnd.Next(0, numberSet.Length);
                password += numberSet[randomNumber];
            }
            if (UseSymbols)
            {
                passwordChars += symbolSet;
                randomNumber = rnd.Next(0, symbolSet.Length);
                password += symbolSet[randomNumber];
            }
            if (UseUppercase)
            {
                passwordChars += characterSet.ToUpper();
                randomNumber = rnd.Next(0, characterSet.Length);
                password += characterSet.ToUpper()[randomNumber];
            }

            // continue building the password until we have the correct length
            passwordChars = _Utils.RandomizeString(passwordChars);

            while (password.Length < PasswordLength)
            {
                randomNumber = rnd.Next(0, passwordChars.Length);
                password += passwordChars[randomNumber];
            }

            return _Utils.RandomizeString(password);
        }

        /// <summary>
        ///  Prompts the user for password requirements and then generates a password.
        /// </summary>
        public void Run()
        {
            bool ReadyToBuild = false;

            GreetUser();

            while (!ReadyToBuild)
            {
                PromptUserForPasswordRequirements();
                ReadyToBuild = CanGenerate();
            }

            Console.WriteLine("\nYour new password is: {0}\n", GeneratePassword());
        }

        /// <summary>
        ///  Instantiates a new PasswordGenerator object and runs the program via the Run() method.
        /// </summary>
        public static void Init()
        {
            _Generator PwdGenerator = new _Generator();
            PwdGenerator.Run();
        }
    }

}
