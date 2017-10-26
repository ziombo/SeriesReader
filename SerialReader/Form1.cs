using System;
using System.Windows.Forms;
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
            var t = new SeriesDownloader(new HttpHandler());


            //var x = await t.GetSeriesDataAsync("ray donovan");
            //var y = await t.ConvertSeriesData(x);

            //var z = await t.GetNextEpisodeDateAsync(y.NextEpLink);
            //var k = await t.AssignNextEpDate(z);
            //label1.Text = k;


            var alfa = await t.GetSeriesAsync(textBox1.Text);

 
            label1.Text = alfa.NextEpDate;


            ////new SeriesDownloader => GetSeriesDataAsync(seriesName) return HttpResponseMessage =>
            ////  ConvertSeriesData(HttpResponseMessage) return SeriesGeneral =>
            ////  GetNextEpisodeDateAsync(linkToNextEpisode) return HttpResponseMessage =>
            ////  AssignNextEpDate(HttpResponseMessage)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = new Random().Next(100, 1000).ToString();
        }
    }
}
