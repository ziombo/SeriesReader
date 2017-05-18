using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReader
{
    interface IGetFilesFromDir
    {
        IEnumerable<string> GetFiles(string path);
    }
}
