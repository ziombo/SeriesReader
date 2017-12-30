using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderConsole.Utils.ConsoleUtil;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.TvShows;

namespace SerialReaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: dodać event do SeriesReader żeby rzucało event jak błąd i wtedy na czilku się dopisujesz elo
            // TODO: przy starcie porównywać daty z DateTime.Now, jeśli inna to ściągać na nowo
            
            ConsoleInteraction.DisplayConsoleMenu();
        }
    }
}
