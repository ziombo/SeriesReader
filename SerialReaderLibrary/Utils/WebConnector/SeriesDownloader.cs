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
    //   ConvertSeriesData(HttpResponseMessage) return MapToSeriesGeneral =>
    //   GetNextEpisodeDateAsync(linkToNextEpisode) return HttpResponseMessage =>
    //   AssignNextEpDate(HttpResponseMessage)

    public class SeriesDownloader
    {
        private readonly IHttpHandler _httpHandler;
        private const string ApiUrlBase = "http://api.tvmaze.com/singlesearch/shows?q=";

        public SeriesDownloader(IHttpHandler httpHandler) => _httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));


        // daje string -> ściągnij info dla tego stringa -> zweryfikuj info -> jeśli złe daj wiadomosć 
        // jeśli dobre sprawdz czy jest link do następnego -> jeśli jest ściągnij


        public async Task<SeriesGeneral> GetSeries(string seriesName)
        {
            HttpResponseMessage seriesInfo = await GetSeriesDataAsync(seriesName);

            SeriesGeneral series = MapToSeriesGeneral(seriesInfo);

            series.NextEpDate = await GetSeriesNextEpDate(series.NextEpLink);

            return series;
        }

        // Ściągnij JSON z api dla danego serialu
        public async Task<HttpResponseMessage> GetSeriesDataAsync(string seriesName)
        {
            string seriesUrl = ApiUrlBase + seriesName;
            return await _httpHandler.GetAsync(seriesUrl);
        }


        private SeriesGeneral MapToSeriesGeneral(HttpResponseMessage seriesInfo)
        {
            HttpResponseHelper.ValidateResponse(seriesInfo);
            return MapSeriesData(seriesInfo);
        }

        private SeriesGeneral MapSeriesData(HttpResponseMessage seriesInfo)
        {
            string seriesDataParsed = seriesInfo.Content.ReadAsStringAsync().Result;
            return JsonConverter<SeriesGeneral>(seriesDataParsed);
        }

        private async Task<string> GetSeriesNextEpDate(string nextEpLink)
        {
            return !String.IsNullOrEmpty(nextEpLink) ? await GetNextEpAsync(nextEpLink) : null;
        }

        private async Task<string> GetNextEpAsync(string seriesNextEpLink)
        {
            HttpResponseMessage nextEpisodeResponse = await GetNextEpDateAsync(seriesNextEpLink);

            HttpResponseHelper.ValidateResponse(nextEpisodeResponse);
            return MapNextEpDate(nextEpisodeResponse);
        }

        private async Task<HttpResponseMessage> GetNextEpDateAsync(string seriesNextEpLink)
        {
            return await _httpHandler.GetAsync(seriesNextEpLink);
        }

        private string MapNextEpDate(HttpResponseMessage nextEpisodeResponse)
        {
            string nextEpDataParsed = nextEpisodeResponse.Content.ReadAsStringAsync().Result;
            return JsonConverter<string>(nextEpDataParsed);
        }


        private T JsonConverter<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>
                (json, new SeriesGeneralJsonConverter(typeof(T)));
        }



        #region BeforeRefactor

        public async Task<SeriesGeneral> GetSeriesAsync(string seriesName)
        {
            HttpResponseMessage seriesResponseMessage = await GetSeriesDataAsync(seriesName);
            if (!HttpResponseHelper.IsResponseOk(seriesResponseMessage))
                return null;

            SeriesGeneral seriesGeneralBase = ConvertSeriesData(seriesResponseMessage);
            if (seriesGeneralBase.NextEpLink == null)
                return null;

            HttpResponseMessage nextEpisodeResponseMessage = await GetNextEpisodeDateAsync(seriesGeneralBase.NextEpLink);
            if (!HttpResponseHelper.IsResponseOk(nextEpisodeResponseMessage))
                return null;

            seriesGeneralBase.NextEpDate = AssignNextEpDate(nextEpisodeResponseMessage);

            return seriesGeneralBase;
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


    }
}
