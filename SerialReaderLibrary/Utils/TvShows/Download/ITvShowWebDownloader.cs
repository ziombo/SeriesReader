using System.Net.Http;
using System.Threading.Tasks;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
    public interface ITvShowWebDownloader
    {
        Task<HttpResponseMessage> GetSeriesDataAsync(string seriesName);
        Task<HttpResponseMessage> GetNextEpisodeDateAsync(string seriesNextEpisodeLink);
    }
}
