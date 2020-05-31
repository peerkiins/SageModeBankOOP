using System;

namespace SageModeBankOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempUsername = string.Empty;
            string tempPassword = string.Empty;
            Bank b = new Bank();
            b.Name = "SageMode";
            Console.WriteLine($"Welcome to {b.Name}");
            switch (ShowMenu("Register", "Login", "Exit"))
            {
                case '1':
                    Console.Clear();
                    Console.WriteLine("[Registration]");
                    Console.Write("Please enter your username: ");
                    tempUsername = Console.ReadLine();
                    if(b.IsAccountExist(tempUsername))
                    {
                        Console.WriteLine("Account already exist...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("Please enter your password: ");
                        tempPassword = Console.ReadLine();
                        b.Register(tempUsername, tempPassword);
                    }
                    break;
                case '2':
                    break;
                case '3':
                    break;
                default:
                    break;
            }
        }

        static char ShowMenu(params string[] items)
        {
            string menuString = "Press ";
            for (int i = 0; i < items.Length; i++)
            {
                string postFix = i == items.Length - 1 ? string.Empty : ", ";
                menuString += $"{i + 1} to {items[i]}{postFix}";
            }
            Console.Write($"{menuString}: ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            return key.KeyChar;
        }
    }
}
