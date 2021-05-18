using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using Crm.Domain.Companies.Employees;

namespace Crm.Application.Contacts
{
    public class RegisterEmployeeContactCommandHandler : ICommandHandler<RegisterEmployeeContactCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterEmployeeContactCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(RegisterEmployeeContactCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            var contactId = company.CreateEmployeeContact(new EmployeeId(request.EmployeeId), request.AddressType, request.Value);

            try
            {
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return contactId.Value;
        }
    }
}