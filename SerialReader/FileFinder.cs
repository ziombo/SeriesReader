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
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        // Get filenames [optional: already sort for movie files. consider]
        // 1) get collection of filenames

        // Consider: separate class | Extract series names from filenames | Consider: alrdy distinct them?


    }
}
