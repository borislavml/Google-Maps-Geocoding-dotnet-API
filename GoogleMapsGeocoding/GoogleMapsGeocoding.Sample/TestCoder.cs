// <copyright file="TestCoder.cs" company="">
// All rights reserved.
// </copyright>
// <author>Alberto Puyana</author>

using GoogleMapsGeocoding.Common;
using System;
using System.Threading.Tasks;

namespace GoogleMapsGeocoding.Sample
{
    /// <summary>
    /// Class to test the coder.
    /// </summary>
    public static class TestCoder
    {
        /// <summary>
        /// Test all the methods of the coder.
        /// </summary>
        /// <returns>Task to await.</returns>
        public static async Task AllAsync()
        {
            string apiKey = string.Empty;

            Geocoder coder = new Geocoder()
            {
                ApiKey = apiKey
            };

            string address = "1600 Amphitheatre Parkway";
            Console.WriteLine($"Geocode address:'{address}' with language and region");
            PrintResponse(await coder.GeocodeAsync(address, language: "en", region: "us"));

            Console.WriteLine(string.Empty);
            Console.WriteLine($"Geocode address:'{address}' with bounds filter");

            // Antioquia bounds 5.418365,-77.135572|8.8814071,-73.871107
            Bounds boundFilter = new Bounds()
            {
                Southwest = new Southwest()
                {
                    Lat = 5.418365,
                    Lng = -77.135572
                },
                Northeast = new Northeast()
                {
                    Lat = 8.8814071,
                    Lng = -73.871107
                }
            };
            PrintResponse(await coder.GeocodeAsync(address, language: "en", boundsBias: boundFilter));

            Console.WriteLine(string.Empty);
            Console.WriteLine($"Geocode address:'{address}' with component filter country");
            PrintResponse(await coder.GeocodeAsync(address, component: new ComponentFilter() { Country = "us" }));

            Console.WriteLine(string.Empty);
            Console.WriteLine($"Geocode address:'{address}' with component filter country and administrative area");
            PrintResponse(await coder.GeocodeAsync(address, component: new ComponentFilter() { Country = "us", AdministravieArea = "California" }));

            Console.WriteLine(string.Empty);
            Console.WriteLine($"Geocode address:'{address}' with component filter country and administrative area, language and department bounds.");
            PrintResponse(await coder.GeocodeAsync(address, component: new ComponentFilter() { Country = "us", AdministravieArea = "California" }, language: "en", boundsBias: boundFilter));

            Console.WriteLine(string.Empty);
            float latitude = 37.4224764f;
            float longitude = -122.0842499f;
            Console.WriteLine($"ReverseGeocode lat:{latitude};long:{longitude}");
            PrintResponse(await coder.ReverseGeocodeAsync(latitude, longitude));
        }

        /// <summary>
        /// Print a response.
        /// </summary>
        /// <param name="respose">Response to use.</param>
        public static void PrintResponse(GeocodeResponse respose)
        {
            if ((respose != null) && (respose.Status == GlobalConstants.OK_STATUS) && (respose.Results != null))
            {
                foreach (var result in respose.Results)
                {
                    Console.WriteLine($"PlaceId:{result.PlaceId}; Types:{string.Join(",", result.Types)}; FormattedAddress:{result.FormattedAddress}");

                    if ((result.Geometry != null) && (result.Geometry.Bounds != null) && result.Geometry.Bounds.HasBounds)
                    {
                        Console.WriteLine($"Bounds:{result.Geometry.Bounds.ToQueryString()}");
                    }
                }
            }
        }
    }
}