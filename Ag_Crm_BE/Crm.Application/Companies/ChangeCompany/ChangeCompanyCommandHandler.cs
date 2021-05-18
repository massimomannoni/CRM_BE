
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using MediatR;

namespace Crm.Application.Companies.ChangeCompany
{
    public class ChangeCompanyCommandHandler : ICommandHandler<ChangeCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(ChangeCompanyCommand request, CancellationToken cancellationToken)
        {

            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.ChangeCompany(request.Name, request.FiscalCode, request.PIva, request.Province, request.City, request.Address, request.Cap, request.ContractType, request.SubScriptionType, request.SubScriptionDate);

            //TODO Commands must be Handled by UnitOfWorkCommand
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }
}
