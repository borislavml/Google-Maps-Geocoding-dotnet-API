using System;

namespace GoogleMapsGeocoding.Common
{
    public class GoogleMapsAuthenticationException : Exception
    {
        public GoogleMapsAuthenticationException(string message)
            : base(message)
        {
        }

        public GoogleMapsAuthenticationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
