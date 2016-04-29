// <copyright file="ComponentFilter.cs" company="">
// All rights reserved.
// </copyright>
// <author>Alberto Puyana</author>

using System.Collections.Generic;
using System.Net;

namespace GoogleMapsGeocoding.Common
{
    /// <summary>
    /// Represents filter for the components in a request.
    /// Filters used here should not be in the address also.
    /// </summary>
    public class ComponentFilter
    {
        /// <summary>
        /// Matches all the administrative_area levels.
        /// </summary>
        public string AdministravieArea { get; set; }

        /// <summary>
        /// Matches a country name or a two letter ISO 3166-1 country code
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Matches against both locality and sublocality types.
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// Matches postal_code and postal_code_prefix.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Matches long or short name of a route.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Returns the filter in query string format.
        /// Example: country:ES|postal_code:12345
        /// </summary>
        /// <returns>String to use as value in the query string.</returns>
        public string ToQueryString()
        {
            string pairFormat = "{0}:{1}";
            string queryString = string.Empty;

            List<string> components = new List<string>();

            if (!string.IsNullOrWhiteSpace(PostalCode))
            {
                components.Add(string.Format(pairFormat, GlobalConstants.POSTAL_CODE_COMPONENT_PARAMETER, WebUtility.UrlEncode(PostalCode)));
            }

            if (!string.IsNullOrWhiteSpace(AdministravieArea))
            {
                components.Add(string.Format(pairFormat, GlobalConstants.ADMINISTRATIVE_AREA_COMPONENT_PARAMETER, WebUtility.UrlEncode(AdministravieArea)));
            }

            if (!string.IsNullOrWhiteSpace(Country))
            {
                components.Add(string.Format(pairFormat, GlobalConstants.COUNTRY_COMPONENT_PARAMETER, WebUtility.UrlEncode(Country)));
            }

            if (!string.IsNullOrWhiteSpace(Locality))
            {
                components.Add(string.Format(pairFormat, GlobalConstants.LOCALITY_COMPONENT_PARAMETER, WebUtility.UrlEncode(Locality)));
            }

            if (!string.IsNullOrWhiteSpace(Route))
            {
                components.Add(string.Format(pairFormat, GlobalConstants.ROUTE_COMPONENT_PARAMETER, WebUtility.UrlEncode(Route)));
            }

            if (components.Count > 0)
            {
                queryString = string.Join("|", components);
            }

            return queryString;
        }
    }
}