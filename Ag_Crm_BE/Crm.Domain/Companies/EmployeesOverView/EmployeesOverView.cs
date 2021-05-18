using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Companies.EmployeesOverViews
{
    public class EmployeesOverView : Entity
    {
        internal EmployeesOverViewId Id;

        private string _contractLevelType;

        private short _employees;

        private bool _isRemoved;

        private EmployeesOverView() {
            _isRemoved = false;
        }

        private EmployeesOverView(string contractLevelType, short employees)
        {
            Id = new EmployeesOverViewId(Guid.NewGuid());
            _contractLevelType = contractLevelType;
            _employees = employees;
            _isRemoved = false;
        }

        internal static EmployeesOverView CreateNew(string contractLevelType, short employees)
        {
            return new EmployeesOverView(contractLevelType, employees);
        }

        internal void Remove()
        {
            _isRemoved = true;
        }
    }
}
