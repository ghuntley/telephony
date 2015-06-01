using System;

namespace Telephony
{
    public class FeatureNotAvailableException : Exception
    {
        public FeatureNotAvailableException()
        {
        }

        public FeatureNotAvailableException(string message)
            : base(message)
        {
        }

        public FeatureNotAvailableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}