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

namespace SerialReaderConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: dodać event do SeriesReader żeby rzucało event jak błąd i wtedy na czilku się dopisujesz elo
			// TODO: przy starcie porównywać daty z DateTime.Now, jeśli inna to ściągać na nowo

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
