using System.Threading.Tasks;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
	public interface ITvShowDownloader
	{
		Task<TvShow> GetTvShowAsync(string tvShowName);
	}
}
