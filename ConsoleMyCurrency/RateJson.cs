using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleMyCurrency
{
    public partial class RateJson
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }
    }

    public partial class RateJson
    {
        public static RateJson FromJson(string json) => JsonConvert.DeserializeObject<RateJson>(json, ConsoleMyCurrency.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RateJson self) => JsonConvert.SerializeObject(self, ConsoleMyCurrency.Converter.Settings);
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


