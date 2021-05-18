
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;

namespace Crm.Application.Companies.RegisterCompany
{
    public class RegisterCompanyCommandHandler : ICommandHandler<RegisterCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CompanyDto> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
        {

            var company = Company.CreateRegistered(request.Name, request.FiscalCode, request.PIva, request.Province, request.City, request.Address, request.Cap, request.ContractType, request.SubScriptionType, request.SubScriptionDate);

            await _companyRepository.CreateAsync(company);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new CompanyDto { Id = company.Id.Value };
        }
    }
}
