// <copyright file="Program.cs" company="">
// All rights reserved.
// </copyright>
// <author>Alberto Puyana</author>

using GoogleMapsGeocoding.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapsGeocoding.Sample
{
    /// <summary>
    /// Sample console app.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Arguments of the program.</param>
        static void Main(string[] args)
        {
            // Create new Geocoder and pass GOOGLE_MAPS_API_KEY(in this example it's stored in .config)
            IGeocoder geocoder = new Geocoder(ConfigurationManager.AppSettings["GOOGLE_MAPS_API_KEY"]);

            // Get GeocodeResponse C# object from address or from Latitude Longitude(reverse geocoding) 
            GeocodeResponse response = geocoder.Geocode("1984 west armitage ave chicago il");
            GeocodeResponse reversGeocoderesponse = geocoder.ReverseGeocode(40.714224, -73.961452);

            // You can then query the response to get what you need
            double latitude = response.Results[0].Geometry.Location.Lat;
            string address = reversGeocoderesponse.Results[1].FormattedAddress;

            // ..or you can get a response in JSON, XML string foramt(for whatever reason) and "play" with it
            string responseJson = geocoder.Geocode("1984 west armitage ave chicago il", ResponseFormat.JSON);
            string reverseResponseXml = geocoder.ReverseGeocode(40.714224, -73.961452, ResponseFormat.XML);

            // Then you can deserialize it to C# object again
            GeocodeResponse geocodeFromJson = geocoder.FromJson(responseJson);
            GeocodeResponse reverseGeocodeFromXml = geocoder.FromXml(reverseResponseXml);
        }
    }
}
