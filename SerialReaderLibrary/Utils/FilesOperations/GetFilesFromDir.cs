using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SerialReaderLibrary.Utils.FilesOperations
{
    public class GetFilesFromDir : IGetFilesFromDir
    {
        private readonly string path;
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
