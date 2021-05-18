using System;


namespace Crm.Application.Addresses
{
    public class AddressesDto
    {
        public Guid Id { get; set; }

        public string AddressType { get; set; }

        public string Value { get; set; }

        public bool IsRemoved { get; set; }
    }
}
