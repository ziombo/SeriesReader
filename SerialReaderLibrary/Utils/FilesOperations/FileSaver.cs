using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReaderLibrary.Utils.FilesOperations
{
    public class FileSaver
    {
        /* Mam liste SeriesGeneral -> formatuje ją w string JSON -> zapisuje string do pliku
            Odpalam program -> sprawdzam czy istnieje plik -> jeśli istnieje to go odczytuje


        */

        private readonly string _location = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                               "/seriesreader.txt";

        public void SaveToAppData(string seriesInJson)
        {
            File.WriteAllText(_location, seriesInJson);
        }

        public string ReadFromAppData()
        {
            return File.ReadAllText(_location);
        }
    }
}
