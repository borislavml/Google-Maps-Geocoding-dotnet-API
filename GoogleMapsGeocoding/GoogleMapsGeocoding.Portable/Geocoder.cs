// <copyright file="Geocoder.cs" company="">
// All rights reserved.
// </copyright>
// <author>Alberto Puyana</author>

using GoogleMapsGeocoding.Common;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleMapsGeocoding
{
    /// <summary>
    /// Geo coder.
    /// </summary>
    public class Geocoder
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Geocoder()
        {
        }

        /// <summary>
        /// Api key to use.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Desrialize json string to GeocodeResponse object
        /// </summary>
        /// <param name="json">json string</param>
        /// <returns>GeocodeResponse object</returns>
        public GeocodeResponse FromJson(string json)
        {
            GeocodeResponse response = null;

            if (!string.IsNullOrEmpty(json))
            {
                response = JsonConvert.DeserializeObject<GeocodeResponse>(json);
            }

            return response;
        }

        /// <summary>
        /// Gets GeoPoint by address
        /// </summary>
        /// <param name="address">Address to search for</param>
        /// <param name="component">Component filter.</param>
        /// <param name="language">Language filter.</param>
        /// <param name="region">Region filter.</param>
        /// <param name="boundsBias">Bounds filter.</param>
        /// <param name="cancellationToken">Cancellation token to use.</param>
        /// <returns>GeocodeResponnse as a sting according to specified format(JSON, XML)</returns>
        public async Task<GeocodeResponse> GeocodeAsync(string address, ComponentFilter component = null, string language = "", string region = "", Bounds boundsBias = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            string response = await GeocodeJsonAsync(address, component, language, region, boundsBias, cancellationToken);

            return FromJson(response);
        }

        /// <summary>
        /// Gets GeoPoint by address
        /// </summary>
        /// <param name="address">Address to search for</param>
        /// <param name="component">Component filter.</param>
        /// <param name="language">Language filter.</param>
        /// <param name="region">Region filter.</param>
        /// <param name="boundsBias">Bounds filter.</param>
        /// <param name="cancellationToken">Cancellation token to use.</param>
        /// <returns>GeocodeResponnse as a sting according to specified format(JSON, XML)</returns>
        public async Task<string> GeocodeJsonAsync(string address, ComponentFilter component = null, string language = "", string region = "", Bounds boundsBias = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(address))
            {
                values.Add(GlobalConstants.ADDRES_REQUEST_PARAM.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty), address);
            }

            if (!string.IsNullOrWhiteSpace(language))
            {
                values.Add(GlobalConstants.LANGUAGE_REQUEST_PARAMETER.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty), language);
            }

            if (!string.IsNullOrWhiteSpace(region))
            {
                values.Add(GlobalConstants.REGION_REQUEST_PARAMETER.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty), region);
            }

            if ((boundsBias != null) && boundsBias.HasBounds)
            {
                values.Add(GlobalConstants.BOUNDS_REQUEST_PARAMETER.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty), boundsBias.ToQueryString());
            }

            if (component != null)
            {
                values.Add(GlobalConstants.COMPONTENTS_REQUEST_PARAMETER.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty), component.ToQueryString());
            }

            var requestUriString = BuildGoogleRequest(values);

            return await GetContentAsync(requestUriString, cancellationToken);
        }

        /// <summary>
        /// Gets address by GeoPoint
        /// </summary>
        /// <param name="latitude">Gepoint latitude</param>
        /// <param name="longitude">Geopoint longitude</param>
        /// <param name="cancellationToken">Cancellation token to use.</param>
        /// <returns>GeocodeResponnse</returns>
        public async Task<GeocodeResponse> ReverseGeocodeAsync(double latitude, double longitude, CancellationToken cancellationToken = default(CancellationToken))
        {
            string response = await ReverseGeocodeJsonAsync(latitude, longitude, cancellationToken);

            return FromJson(response);
        }

        /// <summary>
        /// Gets address by GeoPoint
        /// </summary>
        /// <param name="latitude">Gepoint latitude e.g 40.714224</param>
        /// <param name="longitude">Geopoint longitude e.g -73.961452 </param>
        /// <param name="cancellationToken">Cancellation token to use.</param>
        /// <returns>GeocodeResponnse as a sting according to specified format(JSON, XML)</returns>
        public async Task<string> ReverseGeocodeJsonAsync(double latitude, double longitude, CancellationToken cancellationToken = default(CancellationToken))
        {
            string latLngString = String.Format("{0},{1}", latitude.ToString(), longitude.ToString());
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add(GlobalConstants.LATLNG_REQUEST_PARAM.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty), latLngString);
            var requestUriString = BuildGoogleRequest(values);

            return await GetContentAsync(requestUriString);
        }

        /// <summary>
        /// Get json content for an Uri.
        /// </summary>
        /// <param name="uriRequest">Uri to use.</param>
        /// <param name="cancellationToken">Cancellation token to use.</param>
        /// <param name="ensureSuccessStatusCode">Ensure the response is OK.</param>
        /// <returns></returns>
        protected virtual async Task<string> GetContentAsync(Uri uriRequest, CancellationToken cancellationToken = default(CancellationToken), bool ensureSuccessStatusCode = true)
        {
            string stringContent = string.Empty;

            using (HttpClient httpClient = new HttpClient(new NativeMessageHandler()
            {
                AllowAutoRedirect = true
            }))
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await httpClient.GetAsync(uriRequest, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    if (ensureSuccessStatusCode)
                    {
                        result.EnsureSuccessStatusCode();
                    }

                    stringContent = await result.Content.ReadAsStringAsync();
                }
            }

            return stringContent;
        }

        /// <summary>
        /// Build the request Uri.
        /// </summary>
        /// <param name="requestParamType">Type of request.</param>
        /// <param name="requestParamString">parameter of the request.</param>
        /// <returns></returns>
        private Uri BuildGoogleRequest(Dictionary<string, string> values)
        {
            var requestUri = new UriBuilder(GlobalConstants.GOOGLE_MAPS_REQUST_URI + GlobalConstants.JSON_RESPONSE);

            if (values != null)
            {
                if (!string.IsNullOrWhiteSpace(ApiKey))
                {
                    string keyParam = GlobalConstants.API_KEY_REQUEST_PARAM.Replace("&", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty);

                    if (!values.ContainsKey(keyParam))
                    {
                        values.Add(keyParam, ApiKey);
                    }
                }

                var notEmptyKeys = from value in values
                                   where !string.IsNullOrWhiteSpace(value.Key) && !string.IsNullOrWhiteSpace(value.Value)
                                   select string.Format("{0}={1}", WebUtility.UrlEncode(value.Key), WebUtility.UrlEncode(value.Value));

                if (notEmptyKeys != null && notEmptyKeys.FirstOrDefault() != null)
                {
                    requestUri.Query = string.Join("&", notEmptyKeys);
                }
            }

            return requestUri.Uri;
        }
    }
}