using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record CreateTeamCommand(string Name) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<CreateTeamCommand, Guid>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IManagementUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeamRepository teamRepository, IManagementUserRepository userRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated)
            {
                return Result<Guid>.BadRequest("User is not authenticated");
            }
            var user = _userRepository.GetByIdAsync(_userService.UserId, cancellationToken).Result;
            if (user is null)
            {
                return Result<Guid>.NotFound("User not found");
            }

            var team = Team.Create(request.Name);

            var teamMember = TeamMember.Create(user.Id, team.Id, TeamRole.Admin);
            team.AddMember(teamMember);

            await _teamRepository.AddAsync(team, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<Guid>.Ok(team.Id.Value);
        }
    }
}