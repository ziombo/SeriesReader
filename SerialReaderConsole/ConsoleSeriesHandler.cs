using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialReaderConsole.Utils.ConsoleUtil;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Series.Downloader;

namespace SerialReaderConsole
{
    public class ConsoleSeriesHandler
    {
        private static readonly List<SeriesGeneral> AllSeries = new List<SeriesGeneral>();

        // prevent changing the list, otherwise it was possible to call extension methods on it.
        public static List<SeriesGeneral> GetSeriesCollection()
        {
            return new List<SeriesGeneral>(AllSeries);
        }

        /// <summary>
        /// Checks if series exists in allSeries collection, if not then downloads it from api.
        /// </summary>
        /// <param name="seriesName"></param>
        /// <returns></returns>
        public static SeriesGeneral GetSeriesForConsole(string seriesName)
        {
            SeriesGeneral series = new SeriesGeneral();
            if (!IsSeriesStoredLocally(seriesName, ref series))
                AllSeries.Add(series = DownloadSeries(seriesName));

            return series;
        }

        private static SeriesGeneral DownloadSeries(string seriesName)
        {
            return new SeriesDownloader().GetSeries(seriesName);
        }

        private static bool IsSeriesStoredLocally(string seriesName, ref SeriesGeneral series)
        {
            series = AllSeries.FirstOrDefault(s => s.Name.ToLower() == seriesName.ToLower());
            return series != null;
        }
    }
}
