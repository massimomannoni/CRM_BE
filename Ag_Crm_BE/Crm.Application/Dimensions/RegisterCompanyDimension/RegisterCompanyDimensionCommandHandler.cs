using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;

namespace Crm.Application.Dimensions
{
    public class RegisterCompanyDimensionCommandHandler : ICommandHandler<RegisterCompanyDimensionCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCompanyDimensionCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(RegisterCompanyDimensionCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.RemoveDimensions();

            var dimensionId = company.CreateDimension(request.DimensionType, request.Fee);

            await _unitOfWork.CommitAsync(cancellationToken);


            return dimensionId.Value;
        }

    }
}