using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.Series;
using SerialReaderLibrary.Utils.Series.Mapper;

namespace SerialReaderLibrary.Utils
{
    public static class JsonConverter
    {
        public static T ConvertJsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>
                (json, new SeriesGeneralJsonConverter(typeof(T)));
        }

        public static string ConvertObjectToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
