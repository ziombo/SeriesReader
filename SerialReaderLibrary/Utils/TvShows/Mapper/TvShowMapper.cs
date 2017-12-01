using System.Net.Http;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.TvShows.Mapper
{
    public class TvShowMapper : ITvShowMapper
    {
        public TvShow MapToSeriesGeneral(HttpResponseMessage seriesInfo)
        {
            string seriesDataParsed = seriesInfo.Content.ReadAsStringAsync().Result;
            return JsonConverter.ConvertJsonToObject<TvShow>(seriesDataParsed);
        }

        public string MapToNextEpisodeDate(HttpResponseMessage nextEpisodeResponse)
        {
            string nextEpDataParsed = nextEpisodeResponse.Content.ReadAsStringAsync().Result;
            return JsonConverter.ConvertJsonToObject<string>(nextEpDataParsed);
        }
    }
}
