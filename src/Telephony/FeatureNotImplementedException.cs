using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public class DeviceDoesNotSupportFeatureException : Exception
    {

        public DeviceDoesNotSupportFeatureException()
        {
        }

        public DeviceDoesNotSupportFeatureException(string message) : base(message)
        {
        }

        public DeviceDoesNotSupportFeatureException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
