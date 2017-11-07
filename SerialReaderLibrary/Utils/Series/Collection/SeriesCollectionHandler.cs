using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.Series.Collection
{
    internal static class SeriesCollectionHandler
    {
        public static List<SeriesGeneral> SeriesCollection { get; private set; } = new List<SeriesGeneral>();

        public static bool IsSeriesAlreadyStored(string seriesName, ref SeriesGeneral series)
        {
            series = SeriesCollection.FirstOrDefault(s => s.Name.ToLower() == seriesName.ToLower());
            return series != null;
        }

        public static void AddSeriesToCollection(SeriesGeneral series)
        {
            SeriesCollection.Add(series);
        }

        public static void CreateSeriesCollectionFromJson(List<SeriesGeneral> seriesCollection)
        {
            SeriesCollection = seriesCollection;
        }
    }
}
