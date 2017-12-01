using System.Threading.Tasks;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
	public interface ITvShowDownloader
	{
		Task<TvShow> DownloadSeriesAsync(string seriesName);
		Task<string> GetSeriesNextEpisodeDateAsync(string nextEpisodeLink);
	}
}
