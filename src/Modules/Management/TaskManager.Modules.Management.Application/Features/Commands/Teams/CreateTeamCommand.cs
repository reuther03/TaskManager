using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record CreateTeamCommand : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<CreateTeamCommand, Guid>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IManagementUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeamRepository teamRepository, IManagementUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<Guid>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}