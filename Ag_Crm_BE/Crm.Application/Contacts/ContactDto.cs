using System;

namespace Crm.Application.Contacts
{
    public class ContactDto
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public string ContactType { get; set; }

        public string AddressType { get; set; }

        public string Value { get; set; }

        public bool IsRemoved { get; set; }
    }
}
