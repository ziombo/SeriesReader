using System.Net.Http;
using System.Threading.Tasks;

namespace SerialReaderLibrary.Utils.WebConnector
{
    public class HttpHandler : IHttpHandler
    {
        private readonly HttpClient client = new HttpClient();

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await client.GetAsync(url);
        }
    }
}
