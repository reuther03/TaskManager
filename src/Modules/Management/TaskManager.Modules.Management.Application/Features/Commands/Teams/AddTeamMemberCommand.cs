using System.Text.Json.Serialization;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record AddTeamMemberCommand(
    Guid UserId,
    [property: JsonIgnore]
    Guid UsersTeamId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddTeamMemberCommand, Guid>
    {
        private readonly IManagementUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeamMemberRepository _memberRepository;

        public Handler(IManagementUserRepository userRepository,
            ITeamRepository teamRepository,
            IUnitOfWork unitOfWork,
            ITeamMemberRepository memberRepository,
            IUserService userService)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
            _memberRepository = memberRepository;
            _userService = userService;
        }

        public async Task<Result<Guid>> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated)
                return Result<Guid>.BadRequest("User is not authenticated");

            var currentUser = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);

            if (currentUser is null)
                return Result<Guid>.NotFound("User not found");

            var team = await _teamRepository.GetByIdAsync(TeamId.From(request.UsersTeamId), cancellationToken);
            if (team is null)
                return Result<Guid>.NotFound("Team not found");

            if (currentUser.TeamId != TeamId.From(request.UsersTeamId) && currentUser.TeamRole != TeamRole.Admin)
                return Result<Guid>.BadRequest("You are not allowed to add members to this team");

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
                return Result<Guid>.NotFound("User not found");

            var teamMember = TeamMember.Create(user.Id, team.Id, TeamRole.Member);

            team.AddMember(teamMember);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(team.Id.Value);
        }
    }
}