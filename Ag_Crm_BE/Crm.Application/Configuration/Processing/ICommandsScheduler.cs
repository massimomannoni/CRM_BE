using System.Threading.Tasks;
using MediatR;
using Crm.Application.Configuration.Commands;

namespace Crm.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}