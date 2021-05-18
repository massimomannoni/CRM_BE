using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using MediatR;
using Crm.Domain.Companies.Contacts;
using Crm.Domain.Companies.Employees;

namespace Crm.Application.Contacts
{
    public class RemoveEmployeeContactCommandHandler : ICommandHandler<RemoveEmployeeContactCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveEmployeeContactCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveEmployeeContactCommand request, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.RemoveEmployeeContact(new EmployeeId(request.EmployeeId), new ContactId(request.ContactId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
