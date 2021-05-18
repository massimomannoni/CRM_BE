
using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Domain.SeedWork;
using Crm.Application.Configuration.Commands;
using Crm.Domain.Companies;
using MediatR;
using Crm.Domain.Companies.EmployeesOverViews;

namespace Crm.Application.EmployeesOverViews
{
    public class RemoveCompanyEmployeesOverViewCommandHandler : ICommandHandler<RemoveCompanyEmployeesOverViewCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCompanyEmployeesOverViewCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveCompanyEmployeesOverViewCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(new CompanyId(request.CompanyId));

            company.RemoveEmployeesOverView(new EmployeesOverViewId(request.EmployeesOverViewId));

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
