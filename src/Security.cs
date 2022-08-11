using _Settings;
using System.Collections;

namespace _Security{
    public class Security : PasswordSettings
    {
        private static Dictionary<string, string> _dictionary = new Dictionary<string, string>() {
            {"lowercase", characterSet},
            {"uppercase", characterSet.ToUpper()},
            {"numbers", numberSet},
            {"symbols", symbolSet}
        };

        private static string SymbolWithinFirstSeven(string input)
        {
            // get the first seven chars of the input string
            string firstSeven = input.Substring(0, 7);
            // get all the characters after the first seven
            string afterFirstSeven = input.Substring(7);
            bool hasSymbol = false;

            // check each character to see if they are in the symbol set
            foreach (char c in firstSeven)
            {
                if (symbolSet.Contains(c))
                {
                    hasSymbol = true;
                }
            }

            // if there are no symbols in the first seven characters
            // remove a random char from the first seven characters
            // replace it with a randomly selected symbol
            if(!hasSymbol){
                Random rnd = new Random();
                string symbolSet = _dictionary["symbols"];
                int randomSymbolIndex = rnd.Next(0, symbolSet.Length);
                // since this runs After the first and last character checks
                // we will not mess with the first character in the event our randomly generated
                // symbol is the same as the last character in the password
                int randomFirstSevenIndex = rnd.Next(1, firstSeven.Length);
                string randomSymbol = symbolSet[randomSymbolIndex].ToString();

                firstSeven = firstSeven.Remove(randomFirstSevenIndex, 1);
                firstSeven = firstSeven.Insert(randomFirstSevenIndex, randomSymbol);
            }

            return firstSeven + afterFirstSeven;
        }

        private static string FirstLastCharacterEnforcement(string input)
        {
            // 1.The first and last character must be a letter or symbol
            // 2. The first and last character must be a different type of character
            string output = input;
            string firstCharacter = input.Substring(0, 1);
            string lastCharacter = input.Substring(input.Length - 1, 1);
            bool firstCharIsNumber = _dictionary["numbers"].Contains(firstCharacter);
            bool lastCharIsNumber = _dictionary["numbers"].Contains(lastCharacter);


            Random rnd = new Random();

            string GenRandomChar()
            {
                Dictionary<string, string> tempDictionary = _dictionary;
                tempDictionary.Remove("numbers");
                Dictionary<string, string>.ValueCollection valueColl = tempDictionary.Values;
                string characterPool = "";
                
                foreach (string s in valueColl)
                {
                    characterPool += s;
                }

                // get a random number between 0 and length of character pool
                int randomIndex = rnd.Next(0, characterPool.Length);
                return characterPool[randomIndex].ToString();
            }

            void ReplaceNumber(string input, int removalIndex)
            {
                string tempPassString = GenRandomChar();
                output = input.Remove(removalIndex, 1);
                output = output.Insert(removalIndex, tempPassString.ToString());
            }

            // Ensures that the first and last characters are not equal and are not the same type of character
            void EnsureUniqueness(string input)
            {
                output = input;
                string _firstCharacter = output.Substring(0, 1);
                string _lastCharacter = output.Substring(output.Length - 1, 1);

                bool firstCharIsLower = _dictionary["lowercase"].Contains(_firstCharacter);
                bool firstCharIsUpper = _dictionary["uppercase"].Contains(_firstCharacter);
                bool lastCharIsLower = _dictionary["lowercase"].Contains(_lastCharacter);
                bool lastCharIsUpper = _dictionary["uppercase"].Contains(_lastCharacter);
                bool firstCharIsSymbol = _dictionary["symbols"].Contains(_firstCharacter);
                bool lastCharIsSymbol = _dictionary["symbols"].Contains(_lastCharacter);

                string newCharacter = GenRandomChar();
                // check and make sure that the first and last character are not the same
                if (
                    firstCharIsLower && lastCharIsLower ||
                    firstCharIsUpper && lastCharIsUpper ||
                    firstCharIsSymbol && lastCharIsSymbol
                    )
                {
                    newCharacter = GenRandomChar();
                    output = output.Remove(0, 1);
                    output = output.Insert(0, newCharacter.ToString());
                    EnsureUniqueness(output);
                }
            }

            if (firstCharIsNumber)
            {
                // if the first character is a number, replace it with a letter
                // or symbol at random from the character pool
                ReplaceNumber(output, 0);
            }

            if (lastCharIsNumber)
            {
                // if the last character is a number, replace it with a letter
                // or symbol at random from the character pool
                ReplaceNumber(output, output.Length - 1);
            }

            EnsureUniqueness(output);

            return output;
        }

        /// <summary>
        /// Ensures that: 
        /// 1. The first and last character are a letter or symbol
        /// 2. The first and last character are of a different character set and are not equal
        /// 3. Ensures that the first 7 characters contains at least one symbol
        /// </summary>
        /// <param name="input">Generated password to check</param>
        /// <returns>Returns the password</returns>
        public static string EnforceChecks(string input)
        {
            input = FirstLastCharacterEnforcement(input);
            return SymbolWithinFirstSeven(input);
        }
    }
}