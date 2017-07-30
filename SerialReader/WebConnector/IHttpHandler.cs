using System.Net.Http;
using System.Threading.Tasks;

namespace SerialReader.WebConnector
{
    public interface IHttpHandler
    {
        HttpResponseMessage Get(string url);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
