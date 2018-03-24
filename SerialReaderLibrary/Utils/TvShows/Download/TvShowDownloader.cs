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

		// GetSeries(seriesName: string) => download series -> check resposne -> return mapped series

		public async Task<TvShow> GetSeriesAsync(string seriesName)
		{
			// <1> Download TvShow data: HttpResponseMessage
			HttpResponseMessage tvShowResponseMessage = await _seriesWebDownloader.DownloadSeriesDataAsync(seriesName);
			// </1>

			// <2> Check if Response status is OK. If not -> handle it
			if (!_responseHelper.IsResponseStatusOk(tvShowResponseMessage))
			{
				_responseHelper.HandleError(tvShowResponseMessage);
			}
			// </2>

			// <3> Map HttpResponseMessage to TvShow and return it
			return _seriesMapper.MapToSeriesGeneral(tvShowResponseMessage);
		}


		// Checks if nextEpisodeLink is empty, if not then downloads nextEpisodeData
		public async Task<string> GetSeriesNextEpisodeDateAsync(string nextEpisodeLink)
		{
			if (String.IsNullOrEmpty(nextEpisodeLink))
				return null;

			HttpResponseMessage nextEpisodeResponseMessage = await _seriesWebDownloader.DownloadNextEpisodeDateAsync(nextEpisodeLink);

			if (!_responseHelper.IsResponseStatusOk(nextEpisodeResponseMessage))
			{
				_responseHelper.HandleError(nextEpisodeResponseMessage);
			}

			return _seriesMapper.MapToNextEpisodeDate(nextEpisodeResponseMessage);

		}
	}
}
