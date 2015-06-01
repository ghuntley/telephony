using System;

namespace Telephony
{
    public sealed class EmailRecipient
    {
        public EmailRecipient(string address) : this(address, null)
        {
        }

        public EmailRecipient(string address, string name)
        {
            ParseAddress(address);

            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        public string Address { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return Address;
        }

        private static FormatException CreateFormatException()
        {
            return new FormatException("The specified string is not in the form required for an e-mail address.");
        }

        private void ParseAddress(string address)
        {
            // 1. Quotes for display name
            address = address.Trim();
            var idx = address.IndexOf('"');
            if (idx != -1)
            {
                if (idx != 0 || address.Length == 1)
                    throw CreateFormatException();

                var closing = address.LastIndexOf('"');
                if (closing == idx)
                    throw CreateFormatException();

                if (Name == null)
                    Name = address.Substring(idx + 1, closing - idx - 1).Trim();
                address = address.Substring(closing + 1).Trim();
            }

            // 2. <email>
            idx = address.IndexOf('<');
            if (idx >= 0)
            {
                if (Name == null)
                    Name = address.Substring(0, idx).Trim();
                if (address.Length - 1 == idx)
                    throw CreateFormatException();

                var end = address.IndexOf('>', idx + 1);
                if (end == -1)
                    throw CreateFormatException();

                address = address.Substring(idx + 1, end - idx - 1).Trim();
            }
            Address = address;

            // 3. email
            idx = address.IndexOf('@');
            if (idx <= 0)
                throw CreateFormatException();
            if (idx != address.LastIndexOf('@'))
                throw CreateFormatException();
        }
    }
}