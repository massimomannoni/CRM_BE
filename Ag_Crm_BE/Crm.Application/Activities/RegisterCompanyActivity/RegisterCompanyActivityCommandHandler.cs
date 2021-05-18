using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;

namespace Crm.Application.Activities
{
    public class RegisterCompanyActivityCommandHandler : ICommandHandler<RegisterCompanyActivityCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCompanyActivityCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(RegisterCompanyActivityCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            var activityId = company.CreateActivity(request.ActivityType, request.SectorType, request.Value);

            await _unitOfWork.CommitAsync(cancellationToken);

            return activityId.Value;
        }
    }
}