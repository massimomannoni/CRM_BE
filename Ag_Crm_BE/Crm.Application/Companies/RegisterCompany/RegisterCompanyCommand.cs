using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Companies.RegisterCompany
{
    public class RegisterCompanyCommand : CommandBase<CompanyDto>
    {

        public string Name { get; set; }

        public string FiscalCode { get; set; }

        public string PIva { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Cap { get; set; }

        public string ContractType { get; set; }

        public string SubScriptionType{ get; set; }

        public DateTime SubScriptionDate { get; set; }

        public RegisterCompanyCommand(string name, string fiscal_code, string piva, string province, string city, string address, string cap, string contractType, string subScriptionType, DateTime subScriptionDate)
        {
            Name = name;
            FiscalCode = fiscal_code;
            PIva = piva;
            Province = province;
            City = city;
            Address = address;
            Cap = cap;
            ContractType = contractType;
            SubScriptionType = subScriptionType;
            SubScriptionDate = subScriptionDate;
        }
    }
}
