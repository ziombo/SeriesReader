using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileFinder fileFinder = new FileFinder();
            fileFinder.GetFilenames(@"D:\Resident Evil The Final Chapter (2016) [1080p] [YTS.AG]");
        }
    }
}
