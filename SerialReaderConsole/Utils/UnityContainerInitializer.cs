using System;
using System.Collections.Generic;
using System.Text;
using SerialReaderConsole.TvShowExtensions;
using SerialReaderLibrary.Utils.TvShows.Download;
using SerialReaderLibrary.Utils.TvShows.Mapper;
using SerialReaderLibrary.Utils.WebConnector;
using Unity;

namespace SerialReaderConsole.Utils
{
	internal class UnityContainerInitializer
	{
		internal IUnityContainer InitializeUnityContainer()
		{
			IUnityContainer container = new UnityContainer();

			container.RegisterType<TvShowHandler, TvShowHandler>();
			container.RegisterType<ITvShowDownloader, TvShowDownloader>();
			container.RegisterType<TvShowCache, TvShowCache>();
			container.RegisterType<ITvShowWebDownloader, TvShowWebDownloader>();
			container.RegisterType<ITvShowMapper, TvShowMapper>();
			container.RegisterType<IHttpResponseHelper, HttpResponseHelper>();
			container.RegisterType<IHttpHandler, HttpHandler>();

			return container;
		}
	}
}
