using System.Net.Http;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.WebConnector;

namespace SerialReaderLibrary.Utils.Series.Mapper
{
    public static class SeriesMapper
    {
        public static SeriesGeneral MapToSeriesGeneral(HttpResponseMessage seriesInfo)
        {
            HttpResponseHelper.ValidateResponse(seriesInfo);
            return MapSeriesData(seriesInfo);
        }

        public static string MapNextEpisodeDate(HttpResponseMessage nextEpisodeResponse)
        {
            string nextEpDataParsed = nextEpisodeResponse.Content.ReadAsStringAsync().Result;
            return JsonConverter.ConvertJsonToObject<string>(nextEpDataParsed);
        }

        private static SeriesGeneral MapSeriesData(HttpResponseMessage seriesInfo)
        {
            string seriesDataParsed = seriesInfo.Content.ReadAsStringAsync().Result;
            return JsonConverter.ConvertJsonToObject<SeriesGeneral>(seriesDataParsed);
        }
    }
}
