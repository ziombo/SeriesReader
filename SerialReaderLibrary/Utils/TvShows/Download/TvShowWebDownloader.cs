using System;
using System.Net.Http;
using System.Threading.Tasks;
using SerialReaderLibrary.Utils.WebConnector;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
	public class TvShowWebDownloader : ITvShowWebDownloader
	{
		private const string ApiUrlBase = "http://api.tvmaze.com/singlesearch/shows?embed=nextepisode&q=";
		private readonly IHttpHandler _httpHandler;
		private readonly IHttpResponseHelper _responseHelper;

		public TvShowWebDownloader(IHttpHandler httpHandler, IHttpResponseHelper responseHelper)
		{		
			_httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));
			_responseHelper = responseHelper ?? throw new ArgumentException(nameof(responseHelper));
		}

		public async Task<HttpResponseMessage> DownloadTvShowDataAsync(string seriesName)
		{
			string seriesUrl = ApiUrlBase + seriesName;
			HttpResponseMessage tvShowResponseMessage = await _httpHandler.GetAsync(seriesUrl);
			
			// <2> Check if Response status is OK. If not -> handle it
			if (!_responseHelper.IsResponseStatusOk(tvShowResponseMessage))
			{
				_responseHelper.HandleError(tvShowResponseMessage);
			}
			// </2>

			return tvShowResponseMessage;
		}

		//public async Task<HttpResponseMessage> DownloadNextEpisodeDateAsync(string seriesNextEpisodeLink)
		//{
		//	HttpResponseMessage nextEpisodeResponseMessage = await _httpHandler.GetAsync(seriesNextEpisodeLink);

		//	// <2> Check if Response status is OK. If not -> handle it
		//	if (!_responseHelper.IsResponseStatusOk(nextEpisodeResponseMessage))
		//	{
		//		_responseHelper.HandleError(nextEpisodeResponseMessage);
		//	}
		//	// </2>

		//	return nextEpisodeResponseMessage;
		//	//return await _httpHandler.GetAsync(seriesNextEpisodeLink);
		//}
	}
}
