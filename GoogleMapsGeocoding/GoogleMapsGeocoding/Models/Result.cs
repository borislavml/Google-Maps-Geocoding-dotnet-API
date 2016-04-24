﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace GoogleMapsGeocoding.Common
{
    [Serializable]
    public class Result
    {
        [JsonProperty("address_components")]
        [XmlElement("address_component")]
        public object[] AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        [XmlElement("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        [XmlElement("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        [XmlElement("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        [XmlElement("type")]
        public string[] Types { get; set; }
    }
}
