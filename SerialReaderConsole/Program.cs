using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
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


			IUnityContainer container = new UnityContainer();

			container.RegisterType<TvShowHandler, TvShowHandler>();
			container.RegisterType<ITvShowDownloader, TvShowDownloader>();
			container.RegisterType<TvShowCache, TvShowCache>();
			container.RegisterType<ITvShowWebDownloader, TvShowWebDownloader>();
			container.RegisterType<ITvShowMapper, TvShowMapper>();
			container.RegisterType<IHttpResponseHelper, HttpResponseHelper>();
			container.RegisterType<IHttpHandler, HttpHandler>();


			var seriesHandler = container.Resolve<TvShowHandler>();

			var x = seriesHandler.GetSeries("ray donovan");
			Console.WriteLine(x.Name);
			Console.WriteLine(x.Status);
			//ConsoleInteraction.DisplayConsoleMenu();
		}
	}
}
