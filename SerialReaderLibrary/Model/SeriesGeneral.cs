namespace SerialReaderLibrary.Model
{
    public class SeriesGeneral
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public string NextEpLink { get; set; }

        public string NextEpDate { get; set; }

        public SeriesGeneral(string name = "", string status = "", string link = "")
        {
            Name = name;
            Status = status;
            NextEpLink = link;
        }
    }
}
