using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerialReaderLibrary.Utils.Files.Interfaces;

namespace SerialReaderLibrary.Utils.Files
{
    public class GetFilesFromDir : IGetFilesFromDir
    {
        private readonly string _path;
        public GetFilesFromDir(string path)
        {
            this._path = path;
        }

        public List<string> GetFiles()
        {
            return Directory.GetFiles(_path).ToList();
        }
    }
}
