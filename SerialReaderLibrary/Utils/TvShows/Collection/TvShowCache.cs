using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;

namespace SerialReaderLibrary.Utils.TvShows.Collection
{
	public class TvShowCache
	{
		private static List<TvShow> TvShows { get; set; }

		public TvShowCache()
		{
			TvShows = new List<TvShow>();
		}

		public bool IsSeriesAlreadyStored(string seriesName, ref TvShow series)
		{
			series = TvShows.FirstOrDefault(s => String.Equals(s.Name, seriesName, StringComparison.CurrentCultureIgnoreCase));
			return series != null;
		}

		public void AddSeriesToCollection(TvShow series)
		{
			TvShows.Add(series);
		}

		public IEnumerable<TvShow> GetSeresCollection()
		{
			return TvShows.Select(tvShow => tvShow);
		}

		private static void CreateSeriesCollectionFromJson(List<TvShow> seriesCollection)
		{
			TvShows = seriesCollection;
		}

		public static void LoadCollectionFromFile()
		{
			string seriesCollectionJson = FileOperations.ReadFromAppData();

			if (String.IsNullOrEmpty(seriesCollectionJson))
				return;

			LoadJsonIntoCollection(seriesCollectionJson);
		}

		private static void LoadJsonIntoCollection(string collectionInJson)
		{
			try
			{
				List<JToken> seriesCollection =
					((JArray)JsonConverter.ConvertJsonToObject(collectionInJson)).ToList();

				List<TvShow> seriesCollectionMapped = new List<TvShow>();

				foreach (JToken series in seriesCollection)
				{
					seriesCollectionMapped.Add(((JObject)JsonConverter.ConvertJsonToObject(series.ToString()))
						.ToObject<TvShow>());
				}

				CreateSeriesCollectionFromJson(seriesCollectionMapped);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null) throw ex.InnerException;
				else throw ex;
			}
		}

	}
}
