using System;
using System.Windows.Forms;
using SerialReaderLibrary.ErrorHandling;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Series.Downloader;
using SerialReaderLibrary.Utils.WebConnector;

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
            label1.Text = "";
            SeriesDownloader seriesDownloader = new SeriesDownloader();

            try
            {
                SeriesGeneral alfa = await seriesDownloader.GetSeries(textBox1.Text);
                label1.Text = String.IsNullOrEmpty(alfa.NextEpisodeDate) ? "Next episode unknown" : alfa.NextEpisodeDate;
            }
            catch (DownloadSeriesException exception)
            {
                label1.Text = exception.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = new Random().Next(100, 1000).ToString();
        }
    }
}
