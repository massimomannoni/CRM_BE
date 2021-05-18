using Crm.Application.Activities;
using Crm.Application.Addresses;
using Crm.Application.Contacts;
using Crm.Application.Dimensions;
using Crm.Application.Employees;
using Crm.Application.EmployeesOverViews;
using System;
using System.Collections.Generic;


namespace Crm.Application.Companies.GetCompanyDetails
{
    public class CompanyDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FiscalCode { get; set; }

        public string PIva { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Cap { get; set; }

        public string ContractType { get; set; }

        public string SubScriptionType { get; set; }

        public DateTime SubScriptionDate { get; set; }

        public List<ActivityDto> Activities { get; set; }

        public List<AddressesDto> Addresses { get; set; }

        public List<EmployeeDto> Employees { get; set; }

        public List<DimensionDto> Dimensions { get; set; }

        public List<EmployeesOverViewDto> EmployeesOverViews { get; set; }

    }
}
