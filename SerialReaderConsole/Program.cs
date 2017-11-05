using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderConsole.Utils.ConsoleUtil;
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

            ConsoleInteraction.DisplayConsoleMenu();
        }
    }
}
