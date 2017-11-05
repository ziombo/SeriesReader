using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.Series.Mapper
{
    public class SeriesGeneralJsonConverter : Newtonsoft.Json.JsonConverter
    {
        private readonly Type[] _types;

        public override bool CanWrite => false;


        public SeriesGeneralJsonConverter(params Type[] types)
        {
            _types = types;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            throw new InvalidOperationException("Not going to use this converter to write");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);

                // If airdate is present it means it's checking the nextEpLink already
                // So instead of writting another converter I'm putting it here
                if(item["airdate"] != null)
                {
                    return item["airdate"].Value<string>();
                }

                string name = item["name"]?.Value<string>();
                string status = item["status"]?.Value<string>();
                string nextEpisodeLink = "";

                
                nextEpisodeLink = item["_links"]?["nextepisode"]?["href"]?.Value<string>();


                return new SeriesGeneral(name, status, nextEpisodeLink);
            }
            // IDK what's this about.
            else
            {
                throw new InvalidOperationException("Error in SeriesGeneralJsonConverter. Finally happenned");
            }
        }
    }
}
