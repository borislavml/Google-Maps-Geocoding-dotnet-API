// <copyright file="GlobalConstants.cs" company="">
// All rights reserved.
// </copyright>
// <author></author>

namespace GoogleMapsGeocoding.Common
{
    /// <summary>
    /// Global constants.
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>
        /// Address paramter.
        /// </summary>
        public const string ADDRES_REQUEST_PARAM = "?address=";

        /// <summary>
        /// Component filter key for administrative area.
        /// </summary>
        public const string ADMINISTRATIVE_AREA_COMPONENT_PARAMETER = "administrative_area";

        /// <summary>
        /// Key parameter.
        /// </summary>
        public const string API_KEY_REQUEST_PARAM = "&key=";

        /// <summary>
        /// Bounds request paramter.
        /// </summary>
        public const string BOUNDS_REQUEST_PARAMETER = "bounds";

        /// <summary>
        /// Component request paramter.
        /// </summary>
        public const string COMPONTENTS_REQUEST_PARAMETER = "components";

        /// <summary>
        /// Component filter key for country.
        /// </summary>
        public const string COUNTRY_COMPONENT_PARAMETER = "country";

        /// <summary>
        /// Map request URI.
        /// </summary>
        public const string GOOGLE_MAPS_REQUST_URI = "https://maps.googleapis.com/maps/api/geocode/";

        /// <summary>
        /// Invalid request status.
        /// </summary>
        public const string INVALID_REQUEST_STATUS = "INVALID_REQUEST";

        /// <summary>
        /// Json response.
        /// </summary>
        public const string JSON_RESPONSE = "json";

        /// <summary>
        /// Language request paramter.
        /// </summary>
        public const string LANGUAGE_REQUEST_PARAMETER = "language";

        /// <summary>
        /// Latitude longitude parameter.
        /// </summary>
        public const string LATLNG_REQUEST_PARAM = "?latlng=";

        /// <summary>
        /// Component filter key for locality.
        /// </summary>
        public const string LOCALITY_COMPONENT_PARAMETER = "locality";

        /// <summary>
        /// Ok status.
        /// </summary>
        public const string OK_STATUS = "OK";

        /// <summary>
        /// Over the limit status.
        /// </summary>
        public const string OVER_QUERY_LIMIT_STATUS = "OVER_QUERY_LIMIT";

        /// <summary>
        /// Component filter key for postal code.
        /// </summary>
        public const string POSTAL_CODE_COMPONENT_PARAMETER = "postal_code";

        /// <summary>
        /// Region request paramter.
        /// </summary>
        public const string REGION_REQUEST_PARAMETER = "region";

        /// <summary>
        /// Request denied status.
        /// </summary>
        public const string REQUEST_DENIED_STATUS = "REQUEST_DENIED";

        /// <summary>
        /// Component filter key for route.
        /// </summary>
        public const string ROUTE_COMPONENT_PARAMETER = "route";

        /// <summary>
        /// Unknown error status.
        /// </summary>
        public const string UNKNOWN_ERROR_STATUS = "UNKNOWN_ERROR";

        /// <summary>
        /// Xml response.
        /// </summary>
        public const string XML_RESPONSE = "xml";

        /// <summary>
        /// Zero results status.
        /// </summary>
        public const string ZERO_RESULTS_STATUS = "ZERO_RESULTS";
    }
}