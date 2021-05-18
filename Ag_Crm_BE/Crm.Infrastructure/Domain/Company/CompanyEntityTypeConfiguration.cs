using System;
using Crm.Domain.Companies;
using Crm.Domain.Companies.Activities;
using Crm.Domain.Companies.Addresses;
using Crm.Domain.Companies.Contacts;
using Crm.Domain.Companies.Dimensions;
using Crm.Domain.Companies.Employees;
using Crm.Domain.Companies.EmployeesOverViews;
using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Infrastructure.Domain.Companies
{
    internal sealed class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {

        internal const string ActivitiesList = "_activities";
        internal const string AddressesList = "_addresses";
        internal const string EmployeeList = "_employees";
        internal const string ContactList = "_contacts";
        internal const string DimensionList = "_dimensions";
        internal const string EmployeeOverViewList = "_employeesOverViews";

        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", SchemaNames.Web);
            
            builder.HasKey(c => c.Id);

            builder.Property("_name").HasColumnName("Name");
            builder.Property("_fiscalCode").HasColumnName("FiscalCode");
            builder.Property("_pIva").HasColumnName("Piva");
            builder.Property("_province").HasColumnName("Province");
            builder.Property("_city").HasColumnName("City");
            builder.Property("_address").HasColumnName("Address");
            builder.Property("_cap").HasColumnName("Cap");
            builder.Property("_contractType").HasColumnName("ContractType");
            builder.Property("_subScriptionType").HasColumnName("SubScriptionType");
            builder.Property("_subScriptionDate").HasColumnName("SubScriptionDate");
            builder.Property("_isRemoved").HasColumnName("IsRemoved");

            builder.OwnsMany<Activity>(ActivitiesList, a =>
            {
                a.ToTable("Activities", SchemaNames.Web);
                a.HasKey("Id");
                a.Property<ActivityId>("Id");

                a.WithOwner().HasForeignKey("CompanyId");

                a.Property<string>("_sectorType").HasColumnName("SectorType");
                a.Property<string>("_activityType").HasColumnName("ActivityType");
                a.Property<bool>("_value").HasColumnName("Value");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
            });

            builder.OwnsMany<EmployeesOverView>(EmployeeOverViewList, a =>
            {
                a.ToTable("EmployeesOverView", SchemaNames.Web);
                a.HasKey("Id");
                a.Property<EmployeesOverViewId>("Id");

                a.WithOwner().HasForeignKey("CompanyId");

                a.Property<string>("_contractLevelType").HasColumnName("ContractLevelType");
                a.Property<short>("_employees").HasColumnName("Employees");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
            });

            builder.OwnsMany<Dimension>(DimensionList, a =>
            {
                a.ToTable("Dimensions", SchemaNames.Web);
                a.HasKey("Id");
                a.Property<DimensionId>("Id");

                a.WithOwner().HasForeignKey("CompanyId");

                a.Property<string>("_dimensionType").HasColumnName("DimensionType");
                a.Property<decimal>("_fee").HasColumnName("Fee");
                a.Property<DateTime?>("_expireDate").HasColumnName("ExpireDate");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
            });


            builder.OwnsMany<Address>(AddressesList, a =>
            {
                a.ToTable("Addresses", SchemaNames.Web);
                a.HasKey("Id");
                a.Property<AddressId>("Id");

                a.WithOwner().HasForeignKey("CompanyId");

                a.Property<string>("_addressType").HasColumnName("AddressType");
                a.Property<string>("_value").HasColumnName("Value");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
            });

            builder.OwnsMany<Employee>(EmployeeList, e =>
            {
                e.ToTable("Employees", SchemaNames.Web);
                e.HasKey("Id");
                e.Property<EmployeeId>("Id");

                e.WithOwner().HasForeignKey("CompanyId");

                e.Property<string>("_name").HasColumnName("Name");
                e.Property<string>("_surname").HasColumnName("Surname");
                e.Property<string>("_contactType").HasColumnName("ContactType");
                e.Property<bool>("_isRemoved").HasColumnName("IsRemoved");

                e.OwnsMany<Contact>(ContactList, c =>
                {
                    c.ToTable("Contacts", SchemaNames.Web);
                    c.HasKey("Id");
                    c.Property<ContactId>("Id");

                    c.WithOwner().HasForeignKey("EmployeeId");

                    c.Property<string>("_addressType").HasColumnName("AddressType");
                    c.Property<string>("_value").HasColumnName("Value");
                    c.Property<bool>("_isRemoved").HasColumnName("IsRemoved");

                });
            });
        }
    }
}