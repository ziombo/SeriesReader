using System;
using SerialReaderConsole.Utils;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.TvShows.Download;
using Unity;
using TvShowHandler = SerialReaderConsole.TvShowExtensions.TvShowHandler;

namespace SerialReaderConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: dodać event do SeriesReader żeby rzucało event jak błąd i wtedy na czilku się dopisujesz elo
			// TODO: przy starcie porównywać daty z DateTime.Now, jeśli inna to ściągać na nowo
			// TODO: TvShowHandlerWithLocal - decorator pattern dla Handlera, który bedize sprawdzał lokalną kolekcje.
			// TODO: te funkcje do klasy związanej z console. webowa nie będzie zapisywać do pliku
			//public void SaveCollectionToFile()
			//{
			//	string seriesCollectionJson = JsonConverter.ConvertObjectToJson((List<TvShow>)_tvShowCache.GetSeresCollection());
			//	FileOperations.SaveToAppData(seriesCollectionJson);
			//}

			//public void LoadCollectionFromFile()
			//{
			//	TvShowCache.LoadCollectionFromFile();
			//}

			UnityContainerInitializer containerInitializer = new UnityContainerInitializer();
			IUnityContainer container = containerInitializer.InitializeUnityContainer();

			TvShowDownloader tvShowDownloader = container.Resolve<TvShowDownloader>();


			TvShow tvShow1 = tvShowDownloader.GetTvShowAsync("gotham").Result;
			Console.WriteLine(tvShow1.ToString());
			TvShow tvShow2 = tvShowDownloader.GetTvShowAsync("ray donovan").Result;
			Console.WriteLine(tvShow2);
			Console.ReadKey();
			//ConsoleInteraction.DisplayConsoleMenu();
		}
	}
}
