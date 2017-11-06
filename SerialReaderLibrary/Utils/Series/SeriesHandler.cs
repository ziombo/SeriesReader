using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SerialReaderLibrary.Model;
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

            return series;
        }

//TODO: dodać zapisywanie do kolekcji
    }
}
