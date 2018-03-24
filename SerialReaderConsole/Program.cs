using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderConsole.Utils;
using SerialReaderConsole.Utils.ConsoleUtil;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.TvShows;
using SerialReaderLibrary.Utils.TvShows.Collection;
using SerialReaderLibrary.Utils.TvShows.Download;
using SerialReaderLibrary.Utils.TvShows.Mapper;
using SerialReaderLibrary.Utils.WebConnector;
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

			TvShowHandler seriesHandler = container.Resolve<TvShowHandler>();

			TvShow tvShow = seriesHandler.GetSeries("ray donovan");
			Console.WriteLine(tvShow.ToString());
			Console.ReadKey();
			//ConsoleInteraction.DisplayConsoleMenu();
		}
	}
}
