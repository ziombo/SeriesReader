using System;
using System.Net.Http;
using System.Threading.Tasks;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Series.Mapper;
using SerialReaderLibrary.Utils.WebConnector;

namespace SerialReaderLibrary.Utils.Series.Downloader
{
    public class SeriesDownloader
    {
        private readonly IHttpHandler _httpHandler;
        private const string ApiUrlBase = "http://api.tvmaze.com/singlesearch/shows?q=";


        public SeriesDownloader() => _httpHandler = new HttpHandler();
        public SeriesDownloader(IHttpHandler httpHandler) => _httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));


        public async Task<SeriesGeneral> GetSeries(string seriesName)
        {
            HttpResponseMessage seriesInfo = await GetSeriesDataAsync(seriesName);

            SeriesGeneral series = SeriesMapper.MapToSeriesGeneral(seriesInfo);

            series.NextEpisodeDate = await GetSeriesNextEpDate(series.NextEpisodeLink);

            return series;
        }
       
        private async Task<HttpResponseMessage> GetSeriesDataAsync(string seriesName)
        {
            string seriesUrl = ApiUrlBase + seriesName;
            return await _httpHandler.GetAsync(seriesUrl);
        }

        private async Task<string> GetSeriesNextEpDate(string nextEpisodeLink)
        {
            return !String.IsNullOrEmpty(nextEpisodeLink) ? await GetNextEpisodeAsync(nextEpisodeLink) : null;
        }

        private async Task<string> GetNextEpisodeAsync(string seriesNextEpLink)
        {
            HttpResponseMessage nextEpisodeResponse = await GetNextEpDateAsync(seriesNextEpLink);

            HttpResponseHelper.ValidateResponse(nextEpisodeResponse);
            return SeriesMapper.MapNextEpisodeDate(nextEpisodeResponse);
        }

        private async Task<HttpResponseMessage> GetNextEpDateAsync(string seriesNextEpLink)
        {
            return await _httpHandler.GetAsync(seriesNextEpLink);
        }

        
    }
}
