using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReaderLibrary.ErrorHandling
{
    public class DownloadSeriesException : Exception
    {
        public DownloadSeriesException(string message) : base(message)
        {
        }
    }
}
