using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;

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

        private static void CreateSeriesCollectionFromJson(List<SeriesGeneral> seriesCollection)
        {
            SeriesCollection = seriesCollection;
        }

        public static void LoadCollectionFromFile()
        {
            string seriesCollectionJson = FileOperations.ReadFromAppData();

            if (String.IsNullOrEmpty(seriesCollectionJson))
                return;

            LoadJsonIntoCollection(seriesCollectionJson);
        }

        private static void LoadJsonIntoCollection(string collectionInJson)
        {
            try
            {
                List<JToken> seriesCollection =
                    ((JArray)JsonConverter.ConvertJsonToObject(collectionInJson)).ToList();

                List<SeriesGeneral> seriesCollectionMapped = new List<SeriesGeneral>();

                foreach (JToken series in seriesCollection)
                {
                    seriesCollectionMapped.Add(((JObject)JsonConverter.ConvertJsonToObject(series.ToString()))
                        .ToObject<SeriesGeneral>());
                }

                CreateSeriesCollectionFromJson(seriesCollectionMapped);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) throw ex.InnerException;
                else throw ex;
            }
        }

    }
}
