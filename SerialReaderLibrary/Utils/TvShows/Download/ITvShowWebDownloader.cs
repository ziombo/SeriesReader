using System.Net.Http;
using System.Threading.Tasks;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
    public interface ITvShowWebDownloader
    {
        Task<HttpResponseMessage> DownloadTvShowDataAsync(string seriesName);
        //Task<HttpResponseMessage> DownloadNextEpisodeDateAsync(string seriesNextEpisodeLink);
    }
}
