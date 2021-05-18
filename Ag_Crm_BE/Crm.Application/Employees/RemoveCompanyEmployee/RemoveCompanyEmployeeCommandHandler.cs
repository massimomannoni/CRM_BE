using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using MediatR;
using Crm.Domain.Companies.Employees;

namespace Crm.Application.Employees
{
    public class RemoveCompanyEmployeeCommandHandler : ICommandHandler<RemoveCompanyEmployeeCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCompanyEmployeeCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCompanyEmployeeCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.RemoveEmployee(new EmployeeId(request.EmployeeId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
