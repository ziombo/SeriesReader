using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SerialReaderLibrary.Utils.Files.Interfaces;

namespace SerialReaderLibrary.Utils.Files
{
    public class FileFinder
    {
        // Choose localization (directory where the movies are)
        public string SelectDirectory(IFolderBrowserDialogWrapper dialogWrapper)
        {
            string directory = dialogWrapper.GetPathToDirectory();
            if (!Path.IsPathRooted(directory))
                throw new InvalidOperationException("The given directory is not valid.");

            return directory;
        }

        // Iterate through the directory 
        public List<string> GetFilenames(IGetFilesFromDir getFilesFromDir)
        {
            try
            {
                return getFilesFromDir.GetFiles();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        // Get filenames [optional: already sort for movie files. consider]
        // 1) collection of filenames as argument
        // 2) return files that are movies (extension) 
        public List<string> GetMovieFilesFromAllFiles(List<string> fileNames)
        {
            List<string> movieExtensions = new List<string> { "avi", "mp4", "mkv" };

            // Select fileNames that contain one of the extensions. 
            // Check only extension of each file.
            return fileNames.Where(fn =>
                movieExtensions.Any(me => Path.GetExtension(fn).Contains(me))
                ).ToList();
        }

        // Extract series names from filenames
        public List<string> ExtractSeriesFromFileNames(List<string> fileNames)
        {
            List<string> seriesNames = new List<string>();


            // pattern - name.S03  takes the "name" part
            const string pattern = @".+?(?=\.[S]\d{2})";
            const RegexOptions options = RegexOptions.IgnoreCase;
            Regex regex = new Regex(pattern, options);;

            // such ForEach is faster than normal foreach(var x in y)
            // https://stackoverflow.com/a/5834005/5859562

            fileNames.ForEach(fileName =>
            {
                Match match = regex.Match((fileName));
                if(match.Success)
                {
                    seriesNames.Add(match.Value);
                }
            });

            return seriesNames.Distinct().ToList();
        }
    }
}
