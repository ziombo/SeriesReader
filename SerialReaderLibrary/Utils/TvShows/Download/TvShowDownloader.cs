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
	// oraz metode która przyjmuje string (link do następnego odcinka) i ściąga następny odcinek
	public class TvShowDownloader : ITvShowDownloader
	{
		private readonly ITvShowWebDownloader _seriesWebDownloader;
		private readonly ITvShowMapper _seriesMapper;
		private readonly IHttpResponseHelper _responseHelper;

		public TvShowDownloader(ITvShowWebDownloader seriesWebDownloader, ITvShowMapper seriesMapper, IHttpResponseHelper responseHelper)
		{
			_seriesWebDownloader = seriesWebDownloader ?? throw new ArgumentNullException(nameof(seriesWebDownloader));
			_seriesMapper = seriesMapper ?? throw new ArgumentNullException(nameof(seriesMapper));
			_responseHelper = responseHelper ?? throw new ArgumentNullException(nameof(responseHelper));
		}


		public async Task<TvShow> DownloadSeriesAsync(string seriesName)
		{
			HttpResponseMessage tvShowResponseMessage = await _seriesWebDownloader.GetSeriesDataAsync(seriesName);

			if (!_responseHelper.IsResponseStatusOk(tvShowResponseMessage))
			{
				_responseHelper.HandleError(tvShowResponseMessage);
			}


			return _seriesMapper.MapToSeriesGeneral(tvShowResponseMessage);
		}


		// Checks if nextEpisodeLink is empty, if not then downloads nextEpisodeData
		public async Task<string> GetSeriesNextEpisodeDateAsync(string nextEpisodeLink)
		{
			if (String.IsNullOrEmpty(nextEpisodeLink))
				return null;

			HttpResponseMessage nextEpisodeResponseMessage = await _seriesWebDownloader.GetNextEpisodeDateAsync(nextEpisodeLink);

			if (!_responseHelper.IsResponseStatusOk(nextEpisodeResponseMessage))
			{
				_responseHelper.HandleError(nextEpisodeResponseMessage);
			}

			return _seriesMapper.MapToNextEpisodeDate(nextEpisodeResponseMessage);

		}
	}
}
