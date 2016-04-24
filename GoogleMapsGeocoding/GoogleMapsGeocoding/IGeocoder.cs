using GoogleMapsGeocoding.Common;

namespace GoogleMapsGeocoding
{
    public interface IGeocoder
    {
        string ApiKey { get; }

        GeocodeResponse Geocode(string address);

        GeocodeResponse ReverseGeocode(double latitude, double longitude);

        string Geocode(string address, ResponseFormat responseFormat);

        string ReverseGeocode(double latitude, double longitude, ResponseFormat responseFormat);

        GeocodeResponse FromJson(string json);

        GeocodeResponse FromXml(string xml);
    }
}
