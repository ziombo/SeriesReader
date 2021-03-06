﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SerialReaderLibrary.Model;

namespace SerialReaderLibrary.Utils.TvShows.Mapper
{
	public class TvShowJsonConverter : Newtonsoft.Json.JsonConverter
	{
		private readonly Type[] _types;

		public override bool CanWrite => false;


		public TvShowJsonConverter(params Type[] types)
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

				string name = item["name"]?.Value<string>();
				string status = item["status"]?.Value<string>();
				string nextEpisodeDate = item["_embedded"]?["nextepisode"]?["airdate"]?.Value<string>();

				return new TvShow(name, status, nextEpisodeDate);
			}
			// IDK what's this about.
			else
			{
				throw new InvalidOperationException("Error in SeriesGeneralJsonConverter. Finally happenned");
			}
		}
	}
}
