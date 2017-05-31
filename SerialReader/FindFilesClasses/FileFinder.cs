using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialReader
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

        // Extract series names from filenames | Consider: alrdy distinct them?
        public List<string> ExtractSeriesFromFileNames(List<string> fileNames)
        {
            List<string> seriesNames = new List<string>();

            string pattern = @".+?(?=\.[S]\d{2})";
            RegexOptions options = RegexOptions.IgnoreCase;

            foreach (string fileName in fileNames)
            {
                Match match = Regex.Match(fileName, pattern, options);
                if (match.Success)
                {
                    seriesNames.Add(match.Value);
                }
            }
            return seriesNames.Distinct().ToList();
        }
    }
}
