using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.TvShows.Mapper;
using SerialReaderLibrary.Utils.WebConnector;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
	// Służy do ściągnięcia TvShow. Powinien mieć metode która przyjmuje string i zwraca serie,
	public class TvShowDownloader : ITvShowDownloader
	{
		private readonly ITvShowWebDownloader _tvShowWebDownloader;
		private readonly ITvShowMapper _seriesMapper;

		public TvShowDownloader(ITvShowWebDownloader seriesWebDownloader, ITvShowMapper seriesMapper, IHttpResponseHelper responseHelper)
		{
			_tvShowWebDownloader = seriesWebDownloader ?? throw new ArgumentNullException(nameof(seriesWebDownloader));
			_seriesMapper = seriesMapper ?? throw new ArgumentNullException(nameof(seriesMapper));
		}

		// GetTvShowAsync(tvShowName: string) => download HttpResponseMessage -> Map to TvShow 
		public async Task<TvShow> GetTvShowAsync(string tvShowName)
		{
			HttpResponseMessage tvShowResponseMessage = await _tvShowWebDownloader.DownloadTvShowDataAsync(tvShowName);
			TvShow show = _seriesMapper.MapToSeriesGeneral(tvShowResponseMessage);

			return show;
		}
	}
}
