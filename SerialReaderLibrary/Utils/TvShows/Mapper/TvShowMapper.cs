using System.Net.Http;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.TvShows.Mapper
{
    public class TvShowMapper : ITvShowMapper
    {
        public TvShow MapToTvShow(HttpResponseMessage seriesInfo)
        {
            string tvShowDataParsed = seriesInfo.Content.ReadAsStringAsync().Result;
            return JsonConverter.ConvertJsonToObject<TvShow>(tvShowDataParsed);
        }
    }
}
