using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoginChuchu.Models
{
    public partial class CityModel
    {
        public string ID { get; set; }
        public string CityName { get; set; }
    }
    public partial class CityModel
    {
        public static CityModel FromJson(string json) => JsonConvert.DeserializeObject<CityModel>(json, LoginChuchu.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CityModel self) => JsonConvert.SerializeObject(self, LoginChuchu.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
