using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReader.WebConnector.ResourceClasses;

namespace SerialReader.WebConnector
{
    public class SeriesDownloader
    {
        private readonly IHttpHandler _httpHandler;
        private const string API_URL_BASE = "http://api.tvmaze.com/singlesearch/shows?q=";

        public SeriesDownloader(IHttpHandler httpHandler)
        {
            if (httpHandler == null)
                throw new ArgumentNullException(nameof(httpHandler));

            _httpHandler = httpHandler;
        }

        // Ściągnij JSON z api dla danego serialu

        // Stwórz nowy obiekt (serial) 

        public async Task<HttpResponseMessage> GetSeriesData(string seriesName)
        {
            string seriesUrl = API_URL_BASE + seriesName;

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
