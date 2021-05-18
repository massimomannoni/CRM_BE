using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;

namespace Crm.Application.EmployeesOverViews
{
    public class RegisterCompanyEmployeesOverViewCommandHandler : ICommandHandler<RegisterCompanyEmployeesOverViewCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCompanyEmployeesOverViewCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(RegisterCompanyEmployeesOverViewCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            var employeesOverView = company.CreateEmployeesOverView(request.ContractLevelType, request.Employees);

            await _unitOfWork.CommitAsync(cancellationToken);

            return employeesOverView.Value;
        }
    }
}