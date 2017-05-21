using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialReader
{
    public class FolderBrowser : IFolderBrowserDialogWrapper
    {
        public string GetPathToDirectory()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                return result == DialogResult.OK ? fbd.SelectedPath : String.Empty;
            }
        }
    }
}
