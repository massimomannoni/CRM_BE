using System;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Crm.Infrastructure.Database;
using Newtonsoft.Json;
using Crm.Application.Companies.GetConpanies;

namespace Crm.Infrastructure.Processing.InternalCommands
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly CompaniesContext _companiesContext;

        public CommandsDispatcher(
            IMediator mediator,
            CompaniesContext companiesContext)
        {
            _mediator = mediator;
            _companiesContext = companiesContext;
        }

        public async Task DispatchCommandAsync(Guid id)
        {
            var internalCommand = await _companiesContext.InternalCommands.SingleOrDefaultAsync(x => x.Id == id);

            Type type = Assembly.GetAssembly(typeof(GetCompaniesQuery)).GetType(internalCommand.Type);
            dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

            internalCommand.ProcessedDate = DateTime.UtcNow;

            await _mediator.Send(command);
        }
    }
}