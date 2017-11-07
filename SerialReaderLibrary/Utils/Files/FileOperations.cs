using System;
using System.IO;

namespace SerialReaderLibrary.Utils.Files
{
    public static class FileOperations
    {
        /* 
         * Mam liste SeriesGeneral -> formatuje ją w string JSON -> zapisuje string do pliku
           Odpalam program -> sprawdzam czy istnieje plik -> jeśli istnieje to go odczytuje
        */

        private static readonly string _location = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                               "/seriesreader.txt";

        public static void SaveToAppData(string seriesInJson)
        {
            File.WriteAllText(_location, seriesInJson);
        }

        public static string ReadFromAppData()
        {
            return File.ReadAllText(_location);
        }
    }
}
