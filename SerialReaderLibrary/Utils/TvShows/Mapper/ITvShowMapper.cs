using System.Net.Http;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.TvShows.Mapper
{
	public interface ITvShowMapper
	{
		TvShow MapToSeriesGeneral(HttpResponseMessage responseMessage);
	}
}
