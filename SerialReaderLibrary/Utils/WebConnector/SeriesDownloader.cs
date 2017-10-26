using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderLibrary.ErrorHandling;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.WebConnector
{
    // new SeriesDownloader => GetSeriesDataAsync(seriesName) return HttpResponseMessage =>
    //   ConvertSeriesData(HttpResponseMessage) return SeriesGeneral =>
    //   GetNextEpisodeDateAsync(linkToNextEpisode) return HttpResponseMessage =>
    //   AssignNextEpDate(HttpResponseMessage)

    public class SeriesDownloader
    {
        private readonly IHttpHandler _httpHandler;
        private const string ApiUrlBase = "http://api.tvmaze.com/singlesearch/shows?q=";

        public SeriesDownloader(IHttpHandler httpHandler) => _httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));


        // daje string -> ściągnij info dla tego stringa -> zweryfikuj info -> jeśli złe daj wiadomosć 
        // jeśli dobre sprawdz czy jest link do następnego -> jeśli jest ściągnij


        public async Task<string> GetSeries(string seriesName)
        {
            HttpResponseMessage seriesInfo = await GetSeriesDataAsync(seriesName);
            if (IsResponseOk(seriesInfo))
                return MapSeriesData(seriesInfo);
            else
                throw new DownloadSeriesException("Can't download series");

        }

        private string MapSeriesData(HttpResponseMessage seriesInfo)
        {
            string seriesDataParsed = seriesInfo.Content.ReadAsStringAsync().Result;
            SeriesGeneral series = JsonConvert.DeserializeObject<SeriesGeneral>
                 (seriesDataParsed, new SeriesGeneralJsonConverter(typeof(SeriesGeneral)));

            if (!String.IsNullOrEmpty(series.NextEpLink))
                GetNextEp(series.NextEpLink);

        }

        private async Task<string> GetNextEp(string seriesNextEpLink)
        {
            HttpResponseMessage nextEpisodeResponse = await GetNextEpDateAsync(seriesNextEpLink);
            if(IsResponseOk(nextEpisodeResponse))
                
        }

        private async Task<HttpResponseMessage> GetNextEpDateAsync(string seriesNextEpLink)
        {
            return await _httpHandler.GetAsync(seriesNextEpLink);
        }

        #region BeforeRefactor

        public async Task<SeriesGeneral> GetSeriesAsync(string seriesName)
        {
            HttpResponseMessage seriesResponseMessage = await GetSeriesDataAsync(seriesName);
            if (!IsResponseOk(seriesResponseMessage))
                return null;

            SeriesGeneral seriesGeneralBase = ConvertSeriesData(seriesResponseMessage);
            if (seriesGeneralBase.NextEpLink == null)
                return null;

            HttpResponseMessage nextEpisodeResponseMessage = await GetNextEpisodeDateAsync(seriesGeneralBase.NextEpLink);
            if (!IsResponseOk(nextEpisodeResponseMessage))
                return null;

            seriesGeneralBase.NextEpDate = AssignNextEpDate(nextEpisodeResponseMessage);

            return seriesGeneralBase;
        }

        // Ściągnij JSON z api dla danego serialu
        public async Task<HttpResponseMessage> GetSeriesDataAsync(string seriesName)
        {
            string seriesUrl = ApiUrlBase + seriesName;
            return await _httpHandler.GetAsync(seriesUrl);
        }

        // Wyciągnij informacje nt tego serialu (poprawna nazwa, status, link do następnego odcinka)
        public SeriesGeneral ConvertSeriesData(HttpResponseMessage seriesData)
        {
            string seriesDataParsed = seriesData.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<SeriesGeneral>
                        (seriesDataParsed, new SeriesGeneralJsonConverter(typeof(SeriesGeneral)));
        }

        // Ponownie wykonaj zapytanie, link do next episode
        // Wyciągnij nextEpisodeDate (airdate)
        public async Task<HttpResponseMessage> GetNextEpisodeDateAsync(string nextEpLink)
        {
            return await _httpHandler.GetAsync(nextEpLink);
        }

        public string AssignNextEpDate(HttpResponseMessage nextEpData)
        {
            string nextEpDataParsed = nextEpData.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<string>
                        (nextEpDataParsed, new SeriesGeneralJsonConverter(typeof(string)));
        }

#endregion

        private bool IsResponseOk(HttpResponseMessage hrm)
        {
            return hrm.StatusCode == HttpStatusCode.OK;
        }

    }
}
