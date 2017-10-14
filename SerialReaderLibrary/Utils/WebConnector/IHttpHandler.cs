using System.Net.Http;
using System.Threading.Tasks;

namespace SerialReaderLibrary.Utils.WebConnector
{
    public interface IHttpHandler
    {
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
