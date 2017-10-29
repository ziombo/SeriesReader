namespace SerialReaderLibrary.Model
{
    public class SeriesGeneral
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public string NextEpisodeLink { get; set; }

        public string NextEpisodeDate { get; set; }

        public SeriesGeneral(string name = "", string status = "", string link = "")
        {
            Name = name;
            Status = status;
            NextEpisodeLink = link;
        }
    }
}
