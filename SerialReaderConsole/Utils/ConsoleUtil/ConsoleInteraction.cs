using System;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.TvShow;

namespace SerialReaderConsole.Utils.ConsoleUtil
{
    public static class ConsoleInteraction
    {
        private static readonly SeriesHandler SeriesHandler = new SeriesHandler();

        public static void DisplayConsoleMenu()
        {
            DisplayMainMenu();

            string command = "";
            while (command != "4")
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
                case "h":
                    DisplayMainMenu();
                    break;
                case "1":
                    FindSeries();
                    break;
                case "2":
                    DisplaySeriesCollection();
                    break;
                case "3":
                    SaveCollectionToFile();
                    break;
                case "4":
                    break;
                default:
                    Console.WriteLine("Invalid command. Pres h for help");
                    break;
            }
        }

        private static void SaveCollectionToFile()
        {
            SeriesHandler.SaveCollectionToFile();
        }

        private static void FindSeries()
        {
            string seriesName = GetSeriesNameFromUser();
            TvShow series = SeriesHandler.GetSeries(seriesName);
            DisplaySeriesDetailsToConsole(series);

        }

        public static void DisplayMainMenu()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("1) Download series");
            Console.WriteLine("2) Display series collection");
            Console.WriteLine("3) Save to file");
            Console.WriteLine("4) Exit.");

        }

        private static void DisplaySeriesCollection()
        {
            Console.Clear();
            ConsoleAppearance.WriteToEndOfConsoleLine('═');

            SeriesHandler.GetLocalSeriesCollection().ForEach(DisplaySeriesDetailsToConsole);

            ConsoleAppearance.WriteToEndOfConsoleLine('═');

            // scroll to top
            Console.SetWindowPosition(0, 0);
        }

        private static void DisplaySeriesDetailsToConsole(TvShow tvShow)
        {

            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "Series: ");
            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, tvShow.Name, writeLine: true);

            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "Status: ");
            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, tvShow.Status, writeLine: true);

            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "NextEpisode: ");
            ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, tvShow.NextEpisodeDate ?? "Unknown", writeLine: true);

            if (tvShow.NextEpisodeDate != null)
            {
                ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.Green, "Days left: ");
                ConsoleAppearance.WriteToConsoleColoured(ConsoleColor.White, GetDaysLeft(tvShow.NextEpisodeDate), writeLine: true);
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
