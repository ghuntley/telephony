using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class MailAddressCollection : List<MailAddress>
    {
        public void Add(string addresses)
        {
            if (String.IsNullOrWhiteSpace(addresses))
            {
                throw new ArgumentNullException("addresses", "Supplied argument 'addresses' is null, whitespace or empty.");
            }
            
            foreach (string address in addresses.Split(','))
            {
                this.Add(new MailAddress(address));
            }
        }

        public override string ToString()
        {
            var emailAddresses = new StringBuilder();
            for (int i = 0; i < Count; i += 1)
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