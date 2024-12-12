using System;

namespace StrikeBallShop.ConsoleApp
{
    public static class ConsoleHelper
    {
        public static int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Неверный ввод. Попробуйте снова: ");
            }
            return result;
        }

        public static string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static decimal GetDecimalInput(string prompt)
        {
            Console.Write(prompt);
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("Неверный ввод. Попробуйте снова: ");
            }
            return result;
        }

        public static bool GetBoolInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().ToLower();
            while (input != "да" && input != "нет")
            {
                Console.Write("Неверный ввод. Введите 'Да' или 'Нет': ");
                input = Console.ReadLine().ToLower();
            }
            return input == "да";
        }

        public static void PrintMenuHeader(string header)
        {
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(header);
            Console.WriteLine(new string('=', 40));
        }

        public static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PrintSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}