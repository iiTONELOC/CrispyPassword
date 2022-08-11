using _Generator;

namespace PasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            int num;
            string commandLineArgs = string.Join(',', args);
            bool isNum = int.TryParse(commandLineArgs, out num);
            

            if (num>7 && num <129)
            {
                Generator PwdGenerator = new Generator();
                PwdGenerator.PasswordLength = num;
                Console.WriteLine("\nYour new password is: {0}\n",PwdGenerator.GeneratePassword());
            }
            else
            {
                Generator.Init();
            }
            
        }
    }
}
