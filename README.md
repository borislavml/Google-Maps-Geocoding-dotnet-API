# Google-Maps-Geocoding-dotnet-API
**.NET wrapper for interaction with Google Maps Geocoding Web Services 
Use Google Geocoding and Reversegeocoding services with the ability to retrieve and query data in JSON, XML or POCO(.NET objects) format

## Installation
Source is written and compiled on .NET 4.5 but can be build and used against 4.5.1, 4.5.2 and 4.6
* From source
  - Clone/download repo, build solution, add references to GoogleMapsGeocoding.dll and Newtonsoft.Json.dll(dependency) in your project 
* Via Nuget
 - PM> Install-Package Google.Maps.Geocoding.NET-API  (https://www.nuget.org/packages/Google.Maps.Geocoding.NET-API/1.0.0)

## Usage

```csharp
open IntelliFactory.WebSharper.Google

using GoogleMapsGeocoding;
using GoogleMapsGeocoding.Common;

class Program
{
    static void Main(string[] args)
    {
        // Create new Geocoder and pass GOOGLE_MAPS_API_KEY(in this example it's stored in .config)
        IGeocoder geocoder = new Geocoder(ConfigurationManager.AppSettings["GOOGLE_MAPS_API_KEY"]);
        
        // Get GeocodeResponse C# object from address or from Latitude Longitude(reverse geocoding) 
        GeocodeResponse response =  geocoder.Geocode("1984 west armitage ave chicago il");
        GeocodeResponse reversGeocoderesponse = geocoder.ReverseGeocode(40.714224, -73.961452);

        // You can the query the response to get what you need
        double latitude = response.Results[0].Geometry.Location.Lat;
        string neshto = reversGeocoderesponse.Results[1].FormattedAddress;

        // ..or you can get a response in JSON, XML string foramt(for whatever reason) and "play" with it
        string responseJson = geocoder.Geocode("1984 west armitage ave chicago il", ResponseFormat.JSON);
        string reverseResponseXml = geocoder.ReverseGeocode(40.714224, -73.961452, ResponseFormat.XML);

        // Then you can deserialize it to C# object again
        GeocodeResponse geocodeFromJson = geocoder.FromJson(responseJson);
        GeocodeResponse reverseGeocodeFromXml = geocoder.FromXml(reverseResponseXml);           
    }
}
```

