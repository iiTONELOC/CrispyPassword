using _Settings;

namespace _Utils
{
    public static class Utils
    {
        /// <summary>
        /// Randomizes the order of the characters in the provided string.
        /// </summary>
        /// <param name="input">String to be randomized</param>
        /// <returns>A random ordering of letters contained in the input string</returns>
        public static string RandomizeString(string input)
        {
            string temp = "";
            string original = input;
            Random rnd = new Random();

            while (temp.Length < original.Length)
            {
                // get a random number between 0 and length of password
                int randomIndex = rnd.Next(0, input.Length);
                // add the character at the random index to the randomized string
                temp += input[randomIndex];
                // remove the character at the random index from the character set to avoid duplicates
                input = input.Remove(randomIndex, 1);
            }

            return temp.ToString();
        }
    }

    public static class ErrorMessages
    {
        private static string invalidInput = "\nInvalid input\n";
        private static string invalidPasswordLength = "\nPassword length must be between 16 and 128 characters\n";

        /// <summary>
        /// Provides access to the invalidInput error message.
        /// </summary>
        /// <returns>"Invalid input".</returns>
        public static string InvalidInput
        {
            get
            {
                return invalidInput;
            }
        }

        /// <summary>
        /// Provides access to the invalidPasswordLength error message.
        /// </summary>
        /// <returns>"Password length must be between 8 and 128 characters".</returns>
        public static string InvalidPasswordLength
        {
            get
            {
                return invalidPasswordLength;
            }
        }

        /// <summary>
        /// Prints the specified error message to the console.
        /// </summary>
        /// <param name="message">The error message to print.</param>
        /// <remarks>Error messages are displayed using red text.</remarks>
        public static void Print(string message)
        {
            // print the message using red text
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    internal static class PromptMessages
    {
        /// <summary>
        /// Displays a message to the user asking for the desired length of the new password.
        /// </summary>
        public static void PasswordLength()
        {
            Console.WriteLine("How long would you like your password to be?\nPasswords must be between 16 and 128 characters.\n");
        }
    }

    public class Validate
    {
        /// <summary>
        /// Validates a Yes or No answer. If the answer is not valid, the user is prompted to enter a valid answer.
        /// </summary>
        /// <param name="response">User's input from Console.ReadLine()</param>
        /// <returns>The users response</returns>
        public static string Response(string? response)
        {
            {
                while (response?.ToLower()[0] != 'y' && response?.ToLower()[0] != 'n')
                {
                    ErrorMessages.Print(ErrorMessages.InvalidInput);
                    response = Console.ReadLine();
                }
                return response;
            }
        }

        /// <summary>
        /// Overloads the Response validation method to allow for a custom error message to be displayed.
        /// </summary>
        /// <param name="response">User's input from Console.ReadLine()</param>
        /// <param name="errorMessage">Error message to display to the user if the validation fails</param>
        /// <returns>The users response</returns>
        public static string Response(string? response, string errorMessage)
        {
            {
                while (response?.ToLower() != "y" && response?.ToLower() != "n")
                {
                    ErrorMessages.Print(errorMessage);
                    response = Console.ReadLine();
                }
                return response;
            }
        }

        /// <summary>
        /// Overloads the Response method with a customValidator.
        /// </summary>
        /// <remarks>
        /// Should only be used when not evaluating a yes or no answer.
        /// </remarks>
        /// <param name="response">User's input from Console.ReadLine()</param>
        /// <param name="validate">
        ///     Function that accepts the response, and returns true or false depending on the 
        ///     validity of the user's input. The while loop continues to execute while this evaluates to false
        /// </param>
        public static string? Response(string? response, Func<String?, bool> validate)
        {
            {
                bool isValid = validate(response);
                while (!isValid)
                {
                    ErrorMessages.Print(ErrorMessages.InvalidInput);
                    response = Console.ReadLine();
                    isValid = validate(response);
                }
                return response;
            }
        }

        /// <summary>
        /// Overloads the Response method with a customValidator and error message.
        /// </summary>
        /// <remarks>
        /// Should only be used when not evaluating a yes or no answer.
        /// </remarks>
        /// <param name="response">User's input from Console.ReadLine()</param>
        /// <param name="validate">
        ///     Function that accepts the response, and returns true or false depending on the 
        ///     validity of the user's input. The while loop continues to execute while this evaluates to false
        /// </param>
        /// <param name="errorMessage">Error message to display to the user if the validation fails</param>
        public static string? Response(string? response, Func<String?, bool> validate, string errorMessage)
        {
            {
                bool isValid = validate(response);
                while (!isValid)
                {
                    ErrorMessages.Print(errorMessage);
                    response = Console.ReadLine();
                    isValid = validate(response);
                }
                return response;
            }
        }
    }

    public class Input
    {
        /// <summary>
        /// Returns true or false depending on the validity of the user's input.
        /// </summary>
        /// <param name="input">User's input from Console.ReadLine()</param>
        /// <returns>true if the first character of the input is a 'y', false otherwise</returns>
        static bool _IsTruthy(string input)
        {
            return input.ToLower()[0] == 'y';
        }
        /// <summary>
        /// Prompts the user and return their input.
        /// </summary>
        /// <returns>The user's input or null.</returns>
        /// <param name="message">The message to display to the user.</param>
        public static string? PromptUser(string message)
        {
            Console.WriteLine(message);
            return Validate.Response(Console.ReadLine());
        }

        /// <summary>
        /// Prompts the user. Asks the user for the desired length of the new password.
        /// </summary>
        /// <returns>Length of the password to create as an integer</returns>
        public static int PromptPasswordLength()
        {
            PasswordSettings settings = new PasswordSettings();
            PromptMessages.PasswordLength();
            string? response = Console.ReadLine();

            bool validate(string? response)
            {   
                int passwordLength;
                bool isNumber = int.TryParse(response, out passwordLength);
                bool isValid = passwordLength >= settings.MinLength && passwordLength <= settings.MaxLength;
                return isValid;
            }

            string? validatedResponse = Validate.Response(response, validate, ErrorMessages.InvalidPasswordLength);
            return int.Parse(validatedResponse != null ? validatedResponse : settings.MinLength.ToString());
        }
    }
}