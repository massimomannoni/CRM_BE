
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using MediatR;
using Crm.Domain.Companies.Activities;

namespace Crm.Application.Activities
{
    public class RemoveCompanyActivityCommandHandler : ICommandHandler<RemoveCompanyActivityCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCompanyActivityCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCompanyActivityCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.RemoveActivity(new ActivityId(request.ActivityId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
