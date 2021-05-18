using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Commands;
using Crm.Domain.SeedWork;
using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Crm.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly CompaniesContext _companiesContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(ICommandHandler<T, TResult> decorated,   IUnitOfWork unitOfWork, CompaniesContext companiesContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _companiesContext = companiesContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase<TResult>)
            {
                var internalCommand = await _companiesContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}