using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SerialReaderLibrary.ErrorHandling;

namespace SerialReaderLibrary.Utils.WebConnector
{
    public class HttpResponseHelper : IHttpResponseHelper
    {
        public bool IsResponseStatusOk(HttpResponseMessage hrm)
        {
            return hrm.StatusCode == HttpStatusCode.OK;
        }

        public bool HandleError(HttpResponseMessage responseMessage)
        {
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    throw new DownloadTvShowException("Couldn't find series");
                default:
                    throw new DownloadTvShowException("There was en error");
            }
        }
    }
}
