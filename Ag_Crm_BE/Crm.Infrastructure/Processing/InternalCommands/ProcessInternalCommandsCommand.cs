using MediatR;
using Crm.Application;
using Crm.Application.Configuration.Commands;
using Crm.Infrastructure.Processing.Outbox;

namespace Crm.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}