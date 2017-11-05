using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerialReaderLibrary.Utils.Files.Interfaces;

namespace SerialReaderLibrary.Utils.Files
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
