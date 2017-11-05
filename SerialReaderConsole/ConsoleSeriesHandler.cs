using System;
using System.Collections.Generic;
using System.Text;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Series.Downloader;

namespace SerialReaderConsole
{
    public static class ConsoleSeriesHandler
    {
        public static void DisplaySeriesCollection()
        {
            throw new NotImplementedException();
        }

        public static void GetSeriesForConsole()
        {
            string seriesName = GetSeriesNameFromUser();

            SeriesGeneral series = DownloadSeries(seriesName);

            DisplaySeriesDetailsToConsole(series);
        }

        private static string GetSeriesNameFromUser()
        {
            Console.Write("Input series name: ");
            return Console.ReadLine();
        }

        private static SeriesGeneral DownloadSeries(string seriesName)
        {
            SeriesDownloader seriesDownloader = new SeriesDownloader();

            return seriesDownloader.GetSeries(seriesName);
        }

        private static void DisplaySeriesDetailsToConsole(SeriesGeneral seriesGeneral)
        {
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
    }
}
