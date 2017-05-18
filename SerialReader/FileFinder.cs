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
        /// <summary>
        /// User picks directory he wants scanned for series.
        /// </summary>
        /// <returns>Path as string</returns>
        public string SelectDirectory()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
                else
                {
                    throw new InvalidOperationException("The directory is not valid");
                }
            }
        }

        // Iterate through the directory 
        /// <summary>
        /// Searches for filenames in given directory
        /// </summary>
        /// <param name="path">Path to folder with movie files</param>
        public void GetFilenames(IGetFilesFromDir getFilesFromDir)
        {
            try
            {
                var fileNames = getFilesFromDir.GetFiles();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        // Get filenames [optional: already sort for movie files. consider]


        // Consider: separate class | Extract series names from filenames | Consider: alrdy distinct them?


    }
}
