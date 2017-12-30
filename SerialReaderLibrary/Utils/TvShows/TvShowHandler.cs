using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.TvShows.Collection;
using SerialReaderLibrary.Utils.TvShows.Download;

namespace SerialReaderLibrary.Utils.TvShows
{
	// highest level - responsible for main operation: download series, save series, load series.
	public class TvShowHandler
	{
		private readonly ITvShowDownloader _tvShowDownloader;
		private readonly TvShowCache _tvShowCache;

		public TvShowHandler(ITvShowDownloader tvShowDownloader, TvShowCache tvShowCache)
		{
			_tvShowDownloader = tvShowDownloader ?? throw new ArgumentNullException(nameof(tvShowDownloader));
			_tvShowCache = tvShowCache ?? throw new ArgumentNullException(nameof(tvShowCache));
		}


		public TvShow GetSeries(string seriesName)
		{
			TvShow tvShow = new TvShow();

			// <1> Check if series is already stored in memory. If yes then return it without downloading
			if (_tvShowCache.IsSeriesAlreadyStored(seriesName, ref tvShow))
				return tvShow;
			// </1>

			// <2> Download series from web
			tvShow = _tvShowDownloader.GetSeriesAsync(seriesName).Result;
			// </2>

			// <3> Download nextEpisodeDate (could add: check if link is null)
			tvShow.NextEpisodeDate = _tvShowDownloader.GetSeriesNextEpisodeDateAsync(tvShow.NextEpisodeLink).Result;
			// </3>

			// <4> Add new series to in-memory collection
			_tvShowCache.AddSeriesToCollection(tvShow);
			// </4>

			return tvShow;
		}

		public async Task<TvShow> GetSeriesAsync(string seriesName)
		{
			TvShow series = new TvShow();

			if (_tvShowCache.IsSeriesAlreadyStored(seriesName, ref series))
				return series;


			series = await _tvShowDownloader.GetSeriesAsync(seriesName);

			series.NextEpisodeDate = await _tvShowDownloader.GetSeriesNextEpisodeDateAsync(series.NextEpisodeLink);

			_tvShowCache.AddSeriesToCollection(series);

			return series;
		}

		public List<TvShow> GetLocalSeriesCollection()
		{
			return (List<TvShow>)_tvShowCache.GetSeresCollection();
		}

		public void SaveCollectionToFile()
		{
			string seriesCollectionJson = JsonConverter.ConvertObjectToJson((List<TvShow>)_tvShowCache.GetSeresCollection());
			FileOperations.SaveToAppData(seriesCollectionJson);
		}

		public void LoadCollectionFromFile()
		{
			TvShowCache.LoadCollectionFromFile();
		}
	}
}
