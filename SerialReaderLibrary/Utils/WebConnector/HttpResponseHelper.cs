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
    public static class HttpResponseHelper
    {
        public static bool IsResponseOk(HttpResponseMessage hrm)
        {
            return hrm.StatusCode == HttpStatusCode.OK;
        }

        public static bool ValidateResponse(HttpResponseMessage hrm)
        {
            switch (hrm.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.NotFound:
                    throw new DownloadSeriesException("Couldn't find series");
                default:
                    throw new DownloadSeriesException("There was en error");
            }
        }
    }
}
