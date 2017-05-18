using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SerialReader
{
    public class FileFinder
    {
        public IEnumerable<string> FindMoviesInFileNames(IEnumerable<string> movieNames)
        {
            List<string> foundMovieNames = new List<string>();

            // Extract movie name from each file
            movieNames.ToList().ForEach(mn => foundMovieNames.Add(FindNameInFile(mn)));


            foundMovieNames.ForEach(mn => Console.WriteLine(mn));
            return null;
        }
        //todo osobna klasa na sprawdzania plików, dodac do github
        public IEnumerable<string> FindFilesByExtensions(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);
            IEnumerable<FileInfo> filesAvi = d.GetFiles("*.avi"); //Getting AVI files
            IEnumerable<FileInfo> filesMp4 = d.GetFiles("*.mp4"); //Getting MP4 files
            IEnumerable<FileInfo> filesMkv = d.GetFiles("*.mkv"); //Getting MKV files

            List<string> allFiles = new List<string>();
            allFiles.AddRange(filesAvi.Select(f => f.Name));
            allFiles.AddRange(filesMp4.Select(f => f.Name));
            allFiles.AddRange(filesMkv.Select(f => f.Name));

            return allFiles;
        }

        private string FindNameInFile(string fileName)
        {
            Regex regex = new Regex(@".+[S]\d");
            Match match = regex.Match(fileName);
            if (match.Success)
                return match.Value.Remove(match.Value.Length - 3);

            return null;          
        }
    }
}
