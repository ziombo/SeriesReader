using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReader.WebConnector.ResourceClasses
{
    public class SeriesGeneralJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public SeriesGeneralJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not going to use this to write");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);

                string name = item["name"].Value<string>();
                string status = item["status"].Value<string>();
                string nextEpisodeLink = "";


                if (item["_links"]["nextepisode"] != null)
                {
                    nextEpisodeLink = item["_links"]["nextepisode"]["href"].ToString();
                }

                return new SeriesGeneral(name, status, nextEpisodeLink);
            }
            // IDK what's this about.
            else
            {
                JArray array = JArray.Load(reader);

                return array.ToString();
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}
