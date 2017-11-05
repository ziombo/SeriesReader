using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Series.Downloader;

namespace SerialReaderConsole
{
    public class ConsoleSeriesHandler
    {
        private static List<SeriesGeneral> AllSeries = new List<SeriesGeneral>();


        public static void DisplayConsoleMenu()
        {
            string command = "";
            while (command != "3")
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("1) Download series");
                Console.WriteLine("2) Display series collection");
                Console.WriteLine("3) Exit.");
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
                    GetSeriesForConsole();
                    break;
                case "2":
                    DisplaySeriesCollection();
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }

        private static void DisplaySeriesCollection()
        {
            Console.Clear();
            WriteToEndOfConsoleLine('_');

            AllSeries.ForEach(DisplaySeriesDetailsToConsole);

            WriteToEndOfConsoleLine('_');
        }

        private static void GetSeriesForConsole()
        {
             //dodać sprawdzenie czy w kolekcji juz nie ma po name
             // dodać name wpisany żeby porównywać też
            string seriesName = GetSeriesNameFromUser();

            SeriesGeneral series = DownloadSeries(seriesName);

            DisplaySeriesDetailsToConsole(series);

            if(AllSeries.All(s => s.Name != series.Name))
                AllSeries.Add(series);
        }

        private static string GetSeriesNameFromUser()
        {
            Console.Write("Input series name: ");
            return InputToConsoleInColor(ConsoleColor.Cyan);
        }

        private static SeriesGeneral DownloadSeries(string seriesName)
        {
            return new SeriesDownloader().GetSeries(seriesName);
        }

        private static void DisplaySeriesDetailsToConsole(SeriesGeneral seriesGeneral)
        {
            Console.WriteLine();

            WriteToConsoleColoured(ConsoleColor.Green, "Series: ");
            Console.WriteLine(seriesGeneral.Name);

            WriteToConsoleColoured(ConsoleColor.Green, "Status: ");
            Console.WriteLine(seriesGeneral.Status);

            WriteToConsoleColoured(ConsoleColor.Green, "NextEpisode: ");
            Console.WriteLine(seriesGeneral.NextEpisodeDate ?? "Unknown");

            if (seriesGeneral.NextEpisodeDate != null)
            {
                WriteToConsoleColoured(ConsoleColor.Green, "Days left: ");
                Console.WriteLine(Math.Ceiling((DateTime.Parse(seriesGeneral.NextEpisodeDate) - DateTime.Now).TotalDays));
            }

            Console.WriteLine();
        }


        private static void WriteToConsoleColoured(ConsoleColor color, string message, bool writeLine = false)
        {
            ConsoleColor currentColorHolder = Console.ForegroundColor;
            Console.ForegroundColor = color;

            if (writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);

            Console.ForegroundColor = currentColorHolder;
        }

        private static string InputToConsoleInColor(ConsoleColor color)
        {
            ConsoleColor currentColorHolder = Console.ForegroundColor;
            Console.ForegroundColor = color;

            string input = Console.ReadLine();

            Console.ForegroundColor = currentColorHolder;

            return input;
        }

        private static void WriteToEndOfConsoleLine(char character)
        {
            Console.WriteLine(character.ToString().PadRight(Console.WindowWidth, character));
        }
    }
}
