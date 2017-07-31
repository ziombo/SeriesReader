using SerialReader.WebConnector;
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

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var t = new SeriesDownloader(new HttpHandler());
            var x = await t.GetSeriesData("ray donovan");
            var y = await t.ConvertSeriesData(x);

            var z = await t.GetNextEpisodeDate(y.NextEpLink);
            var k = await t.AssignNextEpDate(z);
        }
    }
}
