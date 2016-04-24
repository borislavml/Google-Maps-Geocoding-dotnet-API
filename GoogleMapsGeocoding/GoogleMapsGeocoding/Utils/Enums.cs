
namespace GoogleMapsGeocoding.Common
{
    public enum ResponseFormat
    {
        XML = 0,
        JSON = 1,
    }

    internal enum RequestParam
    {
        ADDRESS = 0,
        LATLNG = 1,
        API_KEY = 2,
    }

    internal enum ResponseStatusCode
    {
        OK = 0,
        ZERO_RESULTS = 1,
        OVER_QUERY_LIMIT = 2,
        REQUEST_DENIED = 3,
        INVALID_REQUEST = 4,
        UNKNOWN_ERROR = 5,
    }
}
