using System;

namespace SerialReaderLibrary.Model
{
	public class TvShow
	{
		public string Name { get; set; }
		public string Status { get; set; }
		public string NextEpisodeDate { get; set; }

		public TvShow(string name = "", string status = "", string nextEpisodeDate = "")
		{
			Name = name;
			Status = status;
			NextEpisodeDate = nextEpisodeDate;
		}

		public override string ToString()
		{
			return String.Format($"Name: {Name} \r\nStatus: {Status} \r\nNext Episode: {NextEpisodeDate}");
		}
	}
}
