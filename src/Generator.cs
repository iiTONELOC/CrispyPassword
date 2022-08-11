using _Utils;
using _Settings;
using _Security;

namespace _Generator
{
    public class Generator : PasswordSettings
    {
        private string password = "";
        private string characterPool = "";

        private int _passwordLength;

        public Generator()
        {
            password = "";
            characterPool = "";
            _passwordLength = -1;
        }

        public int PasswordLength
        {
            get { return _passwordLength; }
            set { _passwordLength = value; }
        }

        /// <summary>
        /// Prompts and validates user input for Password Length.
        /// </summary>
        private void PromptUserForPasswordLength()
        {
            PasswordLength = Input.PromptPasswordLength();
        }
        private void GreetUser()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Welcome to the Password Generator!");
            Console.WriteLine("===================================");
            Console.WriteLine();
        }

        public string GeneratePassword()
        {
            Random rnd = new Random();
            string[] Sets = { characterSet, numberSet, symbolSet, characterSet.ToUpper() };
            int numRandomRounds = 10;
            int randomNumber;

            foreach (string set in Sets)
            {
                // add the set to the available characters
                characterPool += set;
                // add at least one of each character type to the password
                password += set[rnd.Next(0, set.Length)];
            }

            // randomize the characters in the string
            characterPool = Utils.RandomizeString(characterPool);

            // continue building the password until we have the correct length
            while (password.Length < PasswordLength)
            {
                randomNumber = rnd.Next(0, characterPool.Length);
                password += characterPool[randomNumber];
            }


            for(int i = 0; i < numRandomRounds; i++)
            {
                password = Utils.RandomizeString(password);
            }
            
            return Security.EnforceChecks(password);
        }

        /// <summary>
        ///  Prompts the user for password requirements and then generates a password.
        /// </summary>
        public void Run()
        {
            GreetUser();

            PromptUserForPasswordLength();

            Console.WriteLine("\nYour new password is: {0}\n", GeneratePassword());
        }

        /// <summary>
        ///  Instantiates a new PasswordGenerator object and runs the program via the Run() method.
        /// </summary>
        public static void Init()
        {
            Generator PwdGenerator = new Generator();
            PwdGenerator.Run();
        }
    }
}
