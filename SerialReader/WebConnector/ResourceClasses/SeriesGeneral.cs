using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReader.WebConnector.ResourceClasses
{
    public class SeriesGeneral
    {
        private string name;
        private string status;
        private string nextEpLink;
        private string nextEpDate;

        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
        public string NextEpLink { get => nextEpLink; set => nextEpLink = value; }
        public string NextEpDate { get => nextEpDate; set => nextEpDate = value; }

        public SeriesGeneral(string name = "", string status = "", string link = "")
        {
            Name = name;
            Status = status;
            NextEpLink = link;
        }
    }
}
