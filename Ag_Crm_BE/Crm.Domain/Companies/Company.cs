
using Crm.Domain.Companies.Activities;
using Crm.Domain.Companies.Activities.Events;
using Crm.Domain.Companies.Addresses;
using Crm.Domain.Companies.Addresses.Events;
using Crm.Domain.Companies.Contacts;
using Crm.Domain.Companies.Contacts.Events;
using Crm.Domain.Companies.Dimensions;
using Crm.Domain.Companies.Dimensions.Events;
using Crm.Domain.Companies.Employees;
using Crm.Domain.Companies.Employees.Events;
using Crm.Domain.Companies.EmployeesOverViews;
using Crm.Domain.Companies.EmployeesOverViews.Events;
using Crm.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Domain.Companies
{ 
    public class Company: Entity, IAggregateRoot
    {
        public CompanyId Id { get; private set; }

        private string _name;

        private string _fiscalCode;

        private string _pIva;

        private string _province;

        private string _city;

        private string _address;

        private string _cap;

        private string _contractType;

        private string _subScriptionType;

        private DateTime _subScriptionDate;

        private readonly List<Activity> _activities;

        private readonly List<Address> _addresses;

        private readonly List<Employee> _employees;

        private readonly List<Dimension> _dimensions;

        private readonly List<EmployeesOverView> _employeesOverViews;

        private bool _isRemoved;

        private Company()
        {
            _activities = new List<Activity>();
            _addresses = new List<Address>();
            _employees = new List<Employee>();
            _dimensions = new List<Dimension>();
            _employeesOverViews = new List<EmployeesOverView>();
            _isRemoved = false;
        }

        // COMPANY
        private Company(string name, string fiscal_code, string piva, string province, string city, string address, string cap, string contractType, string subScriptionType, DateTime subScriptionDate)
        {
            Id = new CompanyId(Guid.NewGuid());
            _name = name;
            _fiscalCode = fiscal_code;
            _pIva = piva;
            _province = province;
            _city = city;
            _address = address;
            _cap = cap;
            _contractType = contractType;
            _subScriptionType = subScriptionType;
            _subScriptionDate = subScriptionDate;
            _activities = new List<Activity>();
            _addresses = new List<Address>();
            _employees = new List<Employee>();
            _dimensions = new List<Dimension>();
            _employeesOverViews = new List<EmployeesOverView>();
            _isRemoved = false;

            AddDomainEvent(new CompanyRegisteredEvent(Id));
        }

        public static Company CreateRegistered(string name, string fiscal_code, string piva, string province, string city, string address, string cap , string contractType, string subScriptionType, DateTime subScriptionDate)
        {

            // check here some logic , unique email...

            return new Company(name,  fiscal_code, piva, province, city, address, cap, contractType, subScriptionType, subScriptionDate);
        }

        public void ChangeCompany(string name, string fiscal_code, string piva, string province, string city, string address, string cap,string contractType, string subScriptionType, DateTime subScriptionDate)
        {
            _name = name;
            _fiscalCode = fiscal_code;
            _pIva = piva;
            _province = province;
            _city = city;
            _address = address;
            _cap = cap;
            _contractType = contractType;
            _subScriptionType = subScriptionType;
            _subScriptionDate = subScriptionDate;

            AddDomainEvent(new CompanyChangedEvent(Id));
        }

        public void Remove()
        {
            _isRemoved = true;
        }
        // END

        // CREATE ENTITY RELATIONS
        public ActivityId CreateActivity(string sectorType, string activityType, bool value)
        {
            var activity = Activity.CreateNew(sectorType, activityType, value);

            _activities.Add(activity);

            AddDomainEvent(new ActivityRegisteredEvent(activity.Id, Id));

            return activity.Id;

        }

        public DimensionId CreateDimension(string dimensionType, decimal fee)
        {
            var dimension = Dimension.CreateNew(dimensionType, fee);

            _dimensions.Add(dimension);

            AddDomainEvent(new DimensionRegisteredEvent(dimension.Id, Id));

            return dimension.Id;

        }

        public AddressId CreateAddress(string addressType, string value)
        {
            var address = Address.CreateNew(addressType, value);

            _addresses.Add(address);

            AddDomainEvent(new AddressRegisteredEvent(address.Id, Id));

            return address.Id;
        }

        public EmployeesOverViewId CreateEmployeesOverView(string contractLevelType, short employees)
        {
            var employeesOverView = EmployeesOverView.CreateNew(contractLevelType, employees);

            _employeesOverViews.Add(employeesOverView);

            AddDomainEvent(new EmployeesOverViewRegisteredEvent(employeesOverView.Id, Id));

            return employeesOverView.Id;
        }

        public ContactId CreateEmployeeContact(EmployeeId employeeId, string addressType, string value)
        {
            var employee = _employees.Single(x => x.Id == employeeId);

            var contact = employee.AddContact(addressType, value);

            AddDomainEvent(new ContactRegisteredEvent(contact.Id, employeeId));

            return contact.Id;
        }

        public EmployeeId CreateEmployee(string name, string surname, string contactType)
        {
            var employee = Employee.CreateNew(name, surname, contactType);

            _employees.Add(employee);

            AddDomainEvent(new EmployeeRegisteredEvent(employee.Id, Id));

            return employee.Id;
        }

        // REMOVE ENTITY RELATIONS
        public void RemoveEmployee(EmployeeId employeeId)
        {
            var employee = _employees.Single(x => x.Id == employeeId);

            employee.Remove();

            AddDomainEvent(new EmployeeRemovedEvent(employeeId));
        }

        public void RemoveAddress(AddressId addressId)
        {
            var address = _addresses.Single(x => x.Id == addressId);

            address.Remove();

            AddDomainEvent(new AddressRemovedEvent(addressId));
        }

        public void RemoveActivity(ActivityId activityId)
        {
            var activity = _activities.Single(x => x.Id == activityId);

            activity.Remove();

            AddDomainEvent(new ActivityRemovedEvent(activityId));
        }

        public void RemoveDimension(DimensionId dimensionId)
        {
            var dimension = _dimensions.Single(x => x.Id == dimensionId);

            dimension.Remove();

            AddDomainEvent(new DimensionRemovedEvent(dimensionId));
        }

        public void RemoveDimensions()
        {
            foreach (Dimension dimension in _dimensions)
            {
                dimension.Remove();
                AddDomainEvent(new DimensionRemovedEvent(dimension.Id));
            }
        }

        public void RemoveEmployeeContact(EmployeeId employeeId, ContactId contactId)
        {

            var employee = _employees.Single(x => x.Id == employeeId);

            employee.RemoveContact(contactId);

            AddDomainEvent(new ContactRemovedEvent(contactId));
        }

        public void RemoveEmployeesOverView(EmployeesOverViewId employeesOverViewId)
        {

            var employeesOverView = _employeesOverViews.Single(x => x.Id == employeesOverViewId);

            employeesOverView.Remove();

            AddDomainEvent(new EmployeesOverViewRemovedEvent(employeesOverViewId));
        }
    }
}
