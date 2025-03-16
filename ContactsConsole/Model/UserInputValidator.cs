namespace ContactsConsole.Model
{
    public static class UserInputValidator
    {
        public static string SimpleStringValidation(string prompt, string errorMessage)
        {
            string input;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(prompt);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    return input;
                }
            }
        }

        public static string EmailValidation(string prompt, string errorMessage)
        {
            string input;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(prompt);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                input = Console.ReadLine();
                
                if (input.Contains("@") && input.Contains("."))
                {
                    return input;
                   
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                }
            }
        }
        public static string PhoneValidation(string prompt, string errorMessage)
        {
            string input;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(prompt);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                input = Console.ReadLine();
                
                if (input.All(char.IsDigit))
                {
                    return input;
                   
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                }
            }
        }
        
    }

}