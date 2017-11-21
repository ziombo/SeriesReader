using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.Series.Collection;
using SerialReaderLibrary.Utils.Series.Downloader;
using SerialReaderLibrary.Utils.Series.Mapper;

namespace SerialReaderLibrary.Utils.Series
{
    public class SeriesHandler
    {

        private readonly SeriesDownloader _seriesDownloader;

        public SeriesHandler()
        {
            _seriesDownloader = new SeriesDownloader();
        }

        public SeriesHandler(SeriesDownloader seriesDownloader)
        {
            _seriesDownloader = seriesDownloader;
        }


        public SeriesGeneral GetSeries(string seriesName)
        {
            SeriesGeneral series = new SeriesGeneral();

            if (SeriesCollectionHandler.IsSeriesAlreadyStored(seriesName, ref series))
                return series;


            HttpResponseMessage seriesInfo = _seriesDownloader.GetSeriesDataAsync(seriesName).Result;

            series = SeriesMapper.MapToSeriesGeneral(seriesInfo);

            series.NextEpisodeDate = _seriesDownloader.GetSeriesNextEpDate(series.NextEpisodeLink).Result;

            SeriesCollectionHandler.SeriesCollection.Add(series);

            return series;
        }

        public async Task<SeriesGeneral> GetSeriesAsync(string seriesName)
        {
            SeriesGeneral series = new SeriesGeneral();

            if (SeriesCollectionHandler.IsSeriesAlreadyStored(seriesName, ref series))
                return series;


            HttpResponseMessage seriesInfo = await _seriesDownloader.GetSeriesDataAsync(seriesName);

            series = SeriesMapper.MapToSeriesGeneral(seriesInfo);

            series.NextEpisodeDate = await _seriesDownloader.GetSeriesNextEpDate(series.NextEpisodeLink);

            SeriesCollectionHandler.SeriesCollection.Add(series);

            return series;
        }

        public List<SeriesGeneral> GetLocalSeriesCollection()
        {
            return SeriesCollectionHandler.SeriesCollection;
        }

        public void SaveCollectionToFile()
        {
            string seriesCollectionJson = JsonConverter.ConvertObjectToJson(SeriesCollectionHandler.SeriesCollection);
            FileOperations.SaveToAppData(seriesCollectionJson);
        }

        public void LoadCollectionFromFile()
        {
            string seriesCollectionJson = FileOperations.ReadFromAppData();
            try
            {
                List<JToken> seriesCollection =
                    ((JArray)JsonConverter.ConvertJsonToObject(seriesCollectionJson)).ToList();

                List<SeriesGeneral> seriesCollectionMapped = new List<SeriesGeneral>();

                foreach (JToken series in seriesCollection)
                {
                    seriesCollectionMapped.Add(((JObject)JsonConverter.ConvertJsonToObject(series.ToString())).ToObject<SeriesGeneral>());
                }

                SeriesCollectionHandler.CreateSeriesCollectionFromJson(seriesCollectionMapped);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) throw ex.InnerException;
                else throw ex;

            }

        }
    }
}
