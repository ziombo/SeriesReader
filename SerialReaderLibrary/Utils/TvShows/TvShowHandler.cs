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

		private readonly TvShowDownloader _seriesDownloader;


		public TvShowHandler(TvShowDownloader seriesDownloader)
		{
			_seriesDownloader = seriesDownloader;
		}


		public TvShow GetSeries(string seriesName)
		{
			TvShow series = new TvShow();

			// <1> Check if series is already stored in memory. If yes then return it without downloading
			if (TvShowCollectionHandler.IsSeriesAlreadyStored(seriesName, ref series))
				return series;
			// </1>

			// <2> Download series from web
			series = _seriesDownloader.GetSeriesAsync(seriesName).Result;
			// </2>

			// <3> Download nextEpisodeDate (could add: check if link is null)
			series.NextEpisodeDate = _seriesDownloader.GetSeriesNextEpisodeDateAsync(series.NextEpisodeLink).Result;
			// </3>

			// <4> Add new series to in-memory collection
			TvShowCollectionHandler.SeriesCollection.Add(series);
			// </4>

			return series;
		}

		public async Task<TvShow> GetSeriesAsync(string seriesName)
		{
			TvShow series = new TvShow();

			if (TvShowCollectionHandler.IsSeriesAlreadyStored(seriesName, ref series))
				return series;


			series = await _seriesDownloader.GetSeriesAsync(seriesName);

			series.NextEpisodeDate = await _seriesDownloader.GetSeriesNextEpisodeDateAsync(series.NextEpisodeLink);

			TvShowCollectionHandler.SeriesCollection.Add(series);

			return series;
		}

		public List<TvShow> GetLocalSeriesCollection()
		{
			return TvShowCollectionHandler.SeriesCollection;
		}

		public void SaveCollectionToFile()
		{
			string seriesCollectionJson = JsonConverter.ConvertObjectToJson(TvShowCollectionHandler.SeriesCollection);
			FileOperations.SaveToAppData(seriesCollectionJson);
		}

		public void LoadCollectionFromFile()
		{
			TvShowCollectionHandler.LoadCollectionFromFile();
		}
	}
}
