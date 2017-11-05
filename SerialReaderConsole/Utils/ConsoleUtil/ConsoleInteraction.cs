using System;
using SerialReaderLibrary.Model;

namespace SerialReaderConsole.Utils.ConsoleUtil
{
    public static class ConsoleInteraction
    {
        public static void DisplayConsoleMenu()
        {
            DisplayMainMenu();

            string command = "";
            while (command != "3")
            {
                command = Console.ReadLine();
                Console.WriteLine();

                LaunchCommand(command);
            }
        }

        private static void LaunchCommand(string command)
        {
            switch (command)
            {
                case "1":
                    FindSeries();
                    break;
                case "2":
                    DisplaySeriesCollection();
                    break;
                case "h":
                    DisplayMainMenu();
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("Invalid command. Pres h for help");
                    break;
            }
        }

        private static void FindSeries()
        {
            string seriesName = GetSeriesNameFromUser();
            SeriesGeneral series = ConsoleSeriesHandler.GetSeriesForConsole(seriesName);
            DisplaySeriesDetailsToConsole(series);

        }

        public static void DisplayMainMenu()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("1) Download series");
            Console.WriteLine("2) Display series collection");
            Console.WriteLine("3) Exit.");

        }

        private static void DisplaySeriesCollection()
        {
            Console.Clear();
            ConsoleAppearance.WriteToEndOfConsoleLine('═');

            ConsoleSeriesHandler.GetSeriesCollection().ForEach(DisplaySeriesDetailsToConsole);
            ConsoleSeriesHandler.GetSeriesCollection().Add(new SeriesGeneral());
            ConsoleAppearance.WriteToEndOfConsoleLine('═');

            // scroll to top
            Console.SetWindowPosition(0, 0);
        }

        private static void DisplaySeriesDetailsToConsole(SeriesGeneral seriesGeneral)
        {

            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "Series: ");
            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, seriesGeneral.Name, writeLine: true);

            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "Status: ");
            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, seriesGeneral.Status, writeLine: true);

            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "NextEpisode: ");
            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, seriesGeneral.NextEpisodeDate ?? "Unknown", writeLine: true);

            if (seriesGeneral.NextEpisodeDate != null)
            {
                ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "Days left: ");
                ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, GetDaysLeft(seriesGeneral.NextEpisodeDate), writeLine: true);
            }

            Console.WriteLine();
        }

        private static string GetDaysLeft(string date)
        {
            return Math.Ceiling((DateTime.Parse(date) - DateTime.Now).TotalDays).ToString();
        }

        private static string GetSeriesNameFromUser()
        {
            Console.Write("Input series name: ");
            return ConsoleAppearance.InputToConsoleInColor(ConsoleColor.Cyan);
        }
    }
}
