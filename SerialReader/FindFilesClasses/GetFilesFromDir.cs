using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SerialReader
{
    class GetFilesFromDir : IGetFilesFromDir
    {
        string path;
        public GetFilesFromDir(string _path)
        {
            path = _path;
        }
        public List<string> GetFiles()
        {
            return Directory.GetFiles(path).ToList();
        }
    }
}
