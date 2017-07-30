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
        // Wyciągnij informacje nt tego serialu (poprawna nazwa, status, następny odcinek)
        // Stwórz nowy obiekt (serial) 

        public async Task<HttpResponseMessage> GetSeriesData(string seriesName)
        {
            string seriesUrl = API_URL_BASE + seriesName;

            return await _httpHandler.GetAsync(seriesUrl);
        }

        public async Task<Dictionary<String, Object>> ConvertSeriesData(HttpResponseMessage seriesData)
        {
            string seriesDataParsed = await seriesData.Content.ReadAsStringAsync();

            SeriesGeneral sg = JsonConvert.DeserializeObject<SeriesGeneral>(seriesDataParsed, new SeriesGeneralJsonConverter(typeof(SeriesGeneral)));

            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(seriesDataParsed);
        }

        // Tutaj stowrzyć odpowiednie klasy żeby mapować od razu te wartości. ew w metodzie wyżej.s
        public void ExtractSeriesInfo(Dictionary<String, Object> seriesData)
        {
            string name = seriesData["name"].ToString();
            string status = seriesData["status"].ToString();
        }

    }
}
