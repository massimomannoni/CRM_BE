
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using MediatR;
using Crm.Domain.Companies.Dimensions;

namespace Crm.Application.Dimensions
{
    public class RemoveCompanyDimensionCommandHandler : ICommandHandler<RemoveCompanyDimensionCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCompanyDimensionCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCompanyDimensionCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.RemoveDimension(new DimensionId(request.DimensionId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
