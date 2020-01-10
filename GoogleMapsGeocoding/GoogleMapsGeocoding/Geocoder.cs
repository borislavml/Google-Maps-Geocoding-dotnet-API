using System;
using System.IO;
using System.Net;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using GoogleMapsGeocoding.Common;

namespace GoogleMapsGeocoding
{
    public class Geocoder : IGeocoder
    {
        private string apiKey;

        public string ApiKey
        {
            get { return this.apiKey; }
            private set { this.apiKey = value; }
        }

        public Geocoder(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Gets GeoPoint by address
        /// </summary>
        /// <param name="address">Address to search for</param>
        /// <param name="responseType">Response format(JSON, XML)</param>
        /// <returns>GeocodeResponnse as a sting according to specified format(JSON, XML)</returns>
        public string Geocode(string address, ResponseFormat responseFormat)
        {
            string requestUriString = BuildGoogleRequest(responseFormat, RequestParam.ADDRESS, address);

            return GetGoogleResponse(requestUriString);
        }

        /// <summary>
        /// Gets address by GeoPoint
        /// </summary>
        /// <param name="latitude">Gepoint latitude e.g 40.714224</param>
        /// <param name="longitude">Geopoint longitude e.g -73.961452 </param>
        /// <param name="responseType">Response type(JSON, XML)</param>
        /// <returns>GeocodeResponnse as a sting according to specified format(JSON, XML)</returns>
        public string ReverseGeocode(double latitude, double longitude, ResponseFormat responseFormat)
        {
            string latLngString = String.Format("{0},{1}", latitude.ToString(), longitude.ToString());
            string requestUriString = BuildGoogleRequest(responseFormat, RequestParam.LATLNG, latLngString);

            return GetGoogleResponse(requestUriString);
        }

        /// <summary>
        /// Gets address by GeoPoint
        /// </summary>
        /// <param name="latitude">Gepoint latitude</param>
        /// <param name="longitude">Geopoint longitude</param>
        /// <returns>GeocodeResponnse</returns>
        public GeocodeResponse ReverseGeocode(double latitude, double longitude)
        {
            string response = ReverseGeocode(latitude, longitude, ResponseFormat.JSON);

            return JsonConvert.DeserializeObject<GeocodeResponse>(response);
        }

        /// <summary>
        /// Gets GeoPoint by address
        /// </summary>
        /// <param name="address">Address to search for</param>
        /// <returns>GeocodeResponnse </returns>
        public GeocodeResponse Geocode(string address)
        {
            string response = Geocode(address, ResponseFormat.JSON);

            return JsonConvert.DeserializeObject<GeocodeResponse>(response);
        }

        /// <summary>
        /// Deserialie xml string to GeocodeResponse object
        /// </summary>
        /// <param name="xml">xml string</param>
        /// <returns>GeocodeResponse object</returns>
        public GeocodeResponse FromXml(string xml)
        {
            var xs = new XmlSerializer(typeof(GeocodeResponse));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (GeocodeResponse)xs.Deserialize(ms);
            }
        }

        /// <summary>
        /// Desrialize json string to GeocodeResponse object
        /// </summary>
        /// <param name="json">json string</param>
        /// <returns>GeocodeResponse object</returns>
        public GeocodeResponse FromJson(string json)
        {
            return JsonConvert.DeserializeObject<GeocodeResponse>(json);
        }

        private string BuildGoogleRequest(ResponseFormat responseFormat, RequestParam requestParamType, string requestParamString)
        {
            string responseFormatString = GetResponsTypeStringParam(responseFormat);
            string requestParam = GetRequestParamString(requestParamType);

            return String.Format(CultureInfo.InvariantCulture,
                                 "{0}{1}{2}{3}{4}{5}",
                                  GlobalConstants.GOOGLE_MAPS_REQUST_URI,
                                  responseFormatString,
                                  requestParam,
                                  WebUtility.UrlDecode(requestParamString.Trim()),
                                  GlobalConstants.API_KEY_REQUEST_PARAM,
                                  ApiKey);

        }

        private string GetGoogleResponse(string uri)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(new Uri(uri));
            }
        }

        private string GetResponsTypeStringParam(ResponseFormat responseFormat)
        {
            switch (responseFormat)
            {
                case ResponseFormat.XML:
                    return GlobalConstants.XML_RESPONSE;
                case ResponseFormat.JSON:
                    return GlobalConstants.JSON_RESPONSE;
                default:
                    throw new ArgumentException(String.Format("Unsupported format type {0}", responseFormat.ToString()));
            }
        }

        private string GetRequestParamString(RequestParam requestParam)
        {
            switch (requestParam)
            {
                case RequestParam.ADDRESS:
                    return GlobalConstants.ADDRES_REQUEST_PARAM;
                case RequestParam.LATLNG:
                    return GlobalConstants.LATLNG_REQUEST_PARAM;
                default:
                    throw new ArgumentException(String.Format("Unsupported param type {0}", requestParam.ToString()));
            }
        }
    }
}
