using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.WebConnector
{
    // new SeriesDownloader => GetSeriesDataAsync(seriesName) return HttpResponseMessage =>
    //   ConvertSeriesData(HttpResponseMessage) return SeriesGeneral =>
    //   GetNextEpisodeDate(linkToNextEpisode) return HttpResponseMessage =>
    //   AssignNextEpDate(HttpResponseMessage)

    public class SeriesDownloader
    {
        private readonly IHttpHandler _httpHandler;
        private const string ApiUrlBase = "http://api.tvmaze.com/singlesearch/shows?q=";

        public SeriesDownloader(IHttpHandler httpHandler) => _httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));


        public async Task<SeriesGeneral> GetSeries(string seriesName)
        {
            var seriesResponseMessage = await GetSeriesDataAsync(seriesName);
            var seriesGeneralBase = ConvertSeriesDataS(seriesResponseMessage);
            var nextEpisodeUrl = await GetNextEpisodeDate(seriesGeneralBase.NextEpLink);
            seriesGeneralBase.NextEpDate = AssignNextEpDateS(nextEpisodeUrl);

            return seriesGeneralBase;
            
        }
        #region nie działa tak async
        public HttpResponseMessage GetSeriesDataS(string seriesName)
        {
            string seriesUrl = ApiUrlBase + seriesName;
            return _httpHandler.GetAsync(seriesUrl).Result;
        }

        // Wyciągnij informacje nt tego serialu (poprawna nazwa, status, link do następnego odcinka)
        public SeriesGeneral ConvertSeriesDataS(HttpResponseMessage seriesData)
        {
            string seriesDataParsed = seriesData.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<SeriesGeneral>
                        (seriesDataParsed, new SeriesGeneralJsonConverter(typeof(SeriesGeneral)));
        }

        // Ponownie wykonaj zapytanie, link do next episode
        // Wyciągnij nextEpisodeDate (airdate)
        public HttpResponseMessage GetNextEpisodeDateS(string nextEpLink)
        {
            return _httpHandler.Get(nextEpLink);
        }

        public string AssignNextEpDateS(HttpResponseMessage nextEpData)
        {
            string nextEpDataParsed = nextEpData.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<String>
                        (nextEpDataParsed, new SeriesGeneralJsonConverter(typeof(String)));
        }



#endregion



        // Ściągnij JSON z api dla danego serialu
        public async Task<HttpResponseMessage> GetSeriesDataAsync(string seriesName)
        {
            string seriesUrl = ApiUrlBase + seriesName;
            return await _httpHandler.GetAsync(seriesUrl);
        }

        // Wyciągnij informacje nt tego serialu (poprawna nazwa, status, link do następnego odcinka)
        public async Task<SeriesGeneral> ConvertSeriesData(HttpResponseMessage seriesData)
        {
            string seriesDataParsed = await seriesData.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SeriesGeneral>
                        (seriesDataParsed, new SeriesGeneralJsonConverter(typeof(SeriesGeneral)));
        }

        // Ponownie wykonaj zapytanie, link do next episode
        // Wyciągnij nextEpisodeDate (airdate)
        public async Task<HttpResponseMessage> GetNextEpisodeDate(string nextEpLink)
        {
            return await _httpHandler.GetAsync(nextEpLink);
        }

        public async Task<string> AssignNextEpDate(HttpResponseMessage nextEpData)
        {
            string nextEpDataParsed = await nextEpData.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<String>
                        (nextEpDataParsed, new SeriesGeneralJsonConverter(typeof(String)));
        }
    }
}
