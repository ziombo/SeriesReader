using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderConsole.Utils.ConsoleUtil;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.Series;
using SerialReaderLibrary.Utils.Series.Downloader;

namespace SerialReaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: dodać event do SeriesReader żeby rzucało event jak błąd i wtedy na czilku się dopisujesz elo
            // TODO: Wrapper do obsługi ściągania series (sprawdzanie czy jest lokalnie i pobieranie)
            // TODO: czytanie z pliku i tworzenie kolekcji na starcie
            // TODO: Zrzutowanie JSON na kolekcje
            SeriesHandler seriesHandler = new SeriesHandler();
            seriesHandler.LoadCollectionFromFile();
            
            ConsoleInteraction.DisplayConsoleMenu();
        }
    }
}
