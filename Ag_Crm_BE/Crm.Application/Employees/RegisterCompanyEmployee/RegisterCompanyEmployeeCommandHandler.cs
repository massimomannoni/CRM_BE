using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;

namespace Crm.Application.Employees
{
    public class RegisterCompanyEmployeeCommandHandler : ICommandHandler<RegisterCompanyEmployeeCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCompanyEmployeeCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(RegisterCompanyEmployeeCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            var employeeId = company.CreateEmployee(request.Name, request.Surname, request.ContactType);

            await _unitOfWork.CommitAsync(cancellationToken);

            return employeeId.Value;
        }
    }
}