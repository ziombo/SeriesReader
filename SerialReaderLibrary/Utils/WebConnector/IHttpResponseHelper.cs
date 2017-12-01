using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SerialReaderLibrary.Utils.WebConnector
{
	public interface IHttpResponseHelper
	{
		bool IsResponseStatusOk(HttpResponseMessage hrm);
		bool HandleError(HttpResponseMessage statusCode);	
	}
}
