using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.Series.Downloader;

namespace SerialReaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // dodać event do SeriesReader żeby rzucało event jak błąd i wtedy na czilku się dopisujesz elo

            var xz = new FileOperations();
            var test = xz.ReadFromAppData();
            var test2 = JsonConvert.DeserializeObject<SeriesGeneral>(test);
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
                        ConsoleSeriesHandler.GetSeriesForConsole();
                        break;
                    case "2":
                        ConsoleSeriesHandler.DisplaySeriesCollection();
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        private static void WriteToConsoleColoured(ConsoleColor color, string message, bool writeLine = false)
        {
            ConsoleColor currentColorHolder = Console.ForegroundColor;
            Console.ForegroundColor = color;
            
            if(writeLine)
                Console.WriteLine(message);
            else
                Console.Write(message);

            Console.ForegroundColor = currentColorHolder;
        }
    }
}
