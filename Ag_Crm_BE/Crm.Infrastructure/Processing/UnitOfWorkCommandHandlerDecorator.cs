using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Crm.Application;
using Crm.Domain.SeedWork;
using Crm.Infrastructure.Database;
using Crm.Application.Configuration.Commands;

namespace Crm.Infrastructure.Processing
{
    public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
    {
        private readonly ICommandHandler<T> _decorated;

        private readonly IUnitOfWork _unitOfWork;

        private readonly CompaniesContext _companiesContext;

        public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T> decorated,  IUnitOfWork unitOfWork, CompaniesContext ordersContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _companiesContext = ordersContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await _decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand =
                    await _companiesContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                        cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}