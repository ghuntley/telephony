using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class FeatureNotAvailableException : Exception
    {

        public FeatureNotAvailableException()
        {
        }

        public FeatureNotAvailableException(string message) : base(message)
        {
        }

        public FeatureNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
