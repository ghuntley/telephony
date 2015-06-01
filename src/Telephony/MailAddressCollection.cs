using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class MailAddressCollection : List<MailAddress>
    {
        public void Add(string addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses))
            {
                throw new ArgumentNullException("addresses",
                    "Supplied argument 'addresses' is null, whitespace or empty.");
            }

            foreach (var address in addresses.Split(','))
            {
                Add(new MailAddress(address));
            }
        }

        public override string ToString()
        {
            var emailAddresses = new StringBuilder();
            for (var i = 0; i < Count; i += 1)
            {
                if (i > 0)
                {
                    emailAddresses.Append(", ");
                }

                emailAddresses.Append(this[i]);
            }
            return emailAddresses.ToString();
        }
    }
}