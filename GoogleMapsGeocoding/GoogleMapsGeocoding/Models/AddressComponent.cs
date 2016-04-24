using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace GoogleMapsGeocoding.Common
{
    [Serializable]
    public class AddressComponent
    {
        [JsonProperty("long_name")]
        [XmlElement("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        [XmlElement("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        [XmlElement("type")]
        public string[] Types { get; set; }
    }
}
