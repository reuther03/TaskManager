using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record CreateTeamCommand : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<CreateTeamCommand, Guid>
    {
        public Task<Result<Guid>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}