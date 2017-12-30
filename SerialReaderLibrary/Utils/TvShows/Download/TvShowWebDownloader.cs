﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using SerialReaderLibrary.Utils.WebConnector;

namespace SerialReaderLibrary.Utils.TvShows.Download
{
	public class TvShowWebDownloader : ITvShowWebDownloader
	{
		private const string ApiUrlBase = "http://api.tvmaze.com/singlesearch/shows?q=";
		private readonly IHttpHandler _httpHandler;

		public TvShowWebDownloader(IHttpHandler httpHandler)
		{
			_httpHandler = httpHandler ?? throw new ArgumentNullException(nameof(httpHandler));
		}

		public async Task<HttpResponseMessage> DownloadSeriesDataAsync(string seriesName)
		{
			string seriesUrl = ApiUrlBase + seriesName;
			return await _httpHandler.GetAsync(seriesUrl);
		}

		public async Task<HttpResponseMessage> DownloadNextEpisodeDateAsync(string seriesNextEpisodeLink)
		{
			return await _httpHandler.GetAsync(seriesNextEpisodeLink);
		}
	}
}
