using System;

namespace SerialReaderLibrary.Model
{
	public class TvShow
	{
		public string Name { get; set; }
		public string Status { get; set; }
		public string NextEpisodeLink { get; set; }
		public string NextEpisodeDate { get; set; }

		public TvShow(string name = "", string status = "", string link = "")
		{
			Name = name;
			Status = status;
			NextEpisodeLink = link;
		}

		public override string ToString()
		{
			return String.Format($"Name: {Name} \r\nStatus: {Status}");
		}
	}
}
