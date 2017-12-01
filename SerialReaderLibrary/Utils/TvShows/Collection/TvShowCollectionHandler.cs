using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;

namespace SerialReaderLibrary.Utils.TvShows.Collection
{
    internal static class TvShowCollectionHandler
    {
        public static List<TvShow> SeriesCollection { get; private set; } = new List<TvShow>();

        public static bool IsSeriesAlreadyStored(string seriesName, ref TvShow series)
        {
            series = SeriesCollection.FirstOrDefault(s => s.Name.ToLower() == seriesName.ToLower());
            return series != null;
        }

        public static void AddSeriesToCollection(TvShow series)
        {
            SeriesCollection.Add(series);
        }

        private static void CreateSeriesCollectionFromJson(List<TvShow> seriesCollection)
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

                List<TvShow> seriesCollectionMapped = new List<TvShow>();

                foreach (JToken series in seriesCollection)
                {
                    seriesCollectionMapped.Add(((JObject)JsonConverter.ConvertJsonToObject(series.ToString()))
                        .ToObject<TvShow>());
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
