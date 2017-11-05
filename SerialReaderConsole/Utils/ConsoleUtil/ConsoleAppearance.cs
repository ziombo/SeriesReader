using System;

namespace SerialReaderConsole.Utils.ConsoleUtil
{
    public static class ConsoleAppearance
    {
        public static void WriteToConsoleColoured(ConsoleColor color, string message, bool writeLine = false)
        {
            ConsoleColor currentColorHolder = Console.ForegroundColor;
            Console.ForegroundColor = color;

            if (writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);

            Console.ForegroundColor = currentColorHolder;
        }

        public static string InputToConsoleInColor(ConsoleColor color)
        {
            ConsoleColor currentColorHolder = Console.ForegroundColor;
            Console.ForegroundColor = color;

            string input = Console.ReadLine();

            Console.ForegroundColor = currentColorHolder;

            return input;
        }

        public static void WriteToEndOfConsoleLine(char character)
        {
            Console.WriteLine(character.ToString().PadRight(Console.WindowWidth, character));
        }
    }
}
