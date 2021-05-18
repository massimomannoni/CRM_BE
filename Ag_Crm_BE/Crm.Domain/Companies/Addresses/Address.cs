using Crm.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Crm.Domain.Companies.Addresses
{
    [Owned]
    public class Address : Entity
    {
        internal AddressId Id;

        private string _addressType;

        private string _value;

        private bool _isRemoved;

        private Address()
        {
            _isRemoved = false;   
        }

        private Address(string addressType, string value)
        {
            Id = new AddressId(Guid.NewGuid());
            _addressType = addressType;
            _value = value;
            _isRemoved = false;
        }

        internal static Address CreateNew(string addressType, string value)
        {
            return new Address(addressType, value);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
