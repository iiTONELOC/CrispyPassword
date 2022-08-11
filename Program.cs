using Generator;

// TODO:
// 		○ Passwords must contain at least eight alphanumeric characters.
// 		○ Passwords must contain a combination of uppercase and lowercase letters and numbers.
// 		○ Passwords must contain at least one special character within the first seven characters of the password.
//      ○ Passwords must contain a nonnumeric letter or symbol in the first and last character positions.

namespace PasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _Generator.Init();
        }
    }
}