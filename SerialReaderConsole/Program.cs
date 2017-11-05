using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.FilesOperations;
using SerialReaderLibrary.Utils.Series.Downloader;

namespace SerialReaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // dodać event do SeriesReader żeby rzucało event jak błąd i wtedy na czilku się dopisujesz elo

            var xz = new FileSaver();
            var test = xz.ReadFromAppData();
            var test2 = JsonConvert.DeserializeObject<SeriesGeneral>(test); // nie działa bo name i Name. Jutro: zrobić custom object->json converter
            SeriesFinder();
        }


        private static void SeriesFinder()
        {
            string command = "";
            while (command != "3")
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Available commands:");
                Console.WriteLine("1) Download series");
                Console.WriteLine("2) Display series collection");
                Console.WriteLine("3) Exit.");
                command = Console.ReadLine();

                Console.WriteLine();

                switch (command)
                {
                    case "1":
                        GetSeriesNameFromUser();
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
        }

        private static void DisplaySeriesCollection()
        {
            throw new NotImplementedException();
        }

        private static void GetSeriesNameFromUser()
        {
            Console.Write("Input series name: ");
            string seriesName = Console.ReadLine();

            SeriesDownloader seriesDownloader = new SeriesDownloader();

            SeriesGeneral y = seriesDownloader.GetSeries(seriesName);

            string unknown = "Unknown";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Series: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(y.Name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Status: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(y.Status);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("NextEpisode: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(y.NextEpisodeDate ?? unknown);
            if (y.NextEpisodeDate != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Days left: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(Math.Ceiling((DateTime.Parse(y.NextEpisodeDate) - DateTime.Now).TotalDays));
            }

            var x = new FileSaver();

            x.SaveToAppData(SerialReaderLibrary.Utils.JsonConverter.ConvertObjectToJson(y));
        }
    }
}
