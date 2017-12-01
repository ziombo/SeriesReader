using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReaderLibrary.ErrorHandling
{
    public class DownloadTvShowException : Exception
    {
        public DownloadTvShowException(string message) : base(message)
        {
        }
    }
}
