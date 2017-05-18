using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SerialReader
{
    class GetFilesFromDir : IGetFilesFromDir
    {
        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path).ToList();
        }
    }
}
