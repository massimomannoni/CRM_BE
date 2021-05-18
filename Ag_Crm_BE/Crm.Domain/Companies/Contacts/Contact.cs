using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Companies.Contacts
{
    public class Contact : Entity
    {
        internal ContactId Id;

        private string _addressType;

        private string _value;

        private bool _isRemoved;

        private Contact()
        {
            _isRemoved = false;
        }

        private Contact(string addressType, string value)
        {
            Id = new ContactId(Guid.NewGuid());

            _addressType = addressType;
            _value = value;
            _isRemoved = false;
        }

        internal static Contact CreateNew(string addressType, string value)
        {
            return new Contact(addressType, value);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
