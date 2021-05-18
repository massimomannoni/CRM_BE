using Crm.Domain.Companies.Contacts;
using Crm.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Domain.Companies.Employees
{
    public class Employee : Entity
    {
        internal EmployeeId Id;

        private string _name;

        private string _surname;

        private string _contactType;

        private readonly List<Contact> _contacts;

        private bool _isRemoved;

        private Employee()
        {
            _contacts = new List<Contact>();
            _isRemoved = false;
        }

        private Employee(string name, string surname, string contactType)
        {
            Id = new EmployeeId(Guid.NewGuid());
            _name = name;
            _surname = surname;
            _contactType = contactType;
            _contacts = new List<Contact>();
            _isRemoved = false;
        }

        internal static Employee CreateNew(string name, string surname, string contactType)
        {
            return new Employee(name, surname, contactType);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }

        internal Contact AddContact(string addressType, string value) 
        {
            var contact = Contact.CreateNew(addressType, value);

            _contacts.Add(contact);

            return contact; 
        }

        internal void RemoveContact(ContactId contactId)
        {
            var contact = _contacts.Single(x => x.Id == contactId);

            contact.Remove();

        }
    }
}
