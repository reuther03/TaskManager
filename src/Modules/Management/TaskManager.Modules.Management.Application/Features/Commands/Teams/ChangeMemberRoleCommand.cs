using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record ChangeMemberRoleCommand(Guid CurrentTeamId, Guid MemberId, int Role) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<ChangeMemberRoleCommand, Guid>
    {
        private const int Leader = 1;
        private const int Member = 2;

        private readonly IUserService _userService;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMemberRepository _memberRepository;

        public Handler(IUserService userService, ITeamRepository teamRepository, ITeamMemberRepository memberRepository)
        {
            _userService = userService;
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
        }

        public async Task<Result<Guid>> Handle(ChangeMemberRoleCommand request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated)
                return Result<Guid>.BadRequest("User is not authenticated");

            var currentUser = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);

            if (currentUser.TeamId != TeamId.From(request.CurrentTeamId) && currentUser.TeamRole is not TeamRole.Admin)
                return Result<Guid>.BadRequest("User is not a member of this team or does not have the necessary permissions");

            var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if (member is null)
                return Result<Guid>.NotFound("Member not found");

            var team = await _teamRepository.GetByIdAsync(TeamId.From(request.CurrentTeamId), cancellationToken);
            if (team is null)
                return Result<Guid>.NotFound("Team not found");

            if (!await _memberRepository.InSameTeamAsync(currentUser.UserId, member.UserId, team.Id, cancellationToken))
                return Result<Guid>.BadRequest("Member is not a member of this team");

            if (request.Role != Leader && request.Role != Member) // 1 - Leader, 2 - Member
                return Result<Guid>.BadRequest("Invalid role");

            if (member.TeamRole == TeamRole.Admin)
                return Result<Guid>.BadRequest("Cannot change role of admin");


            var memberRole = request.Role == Leader ? TeamRole.Leader : TeamRole.Member;
            member.ChangeRole(memberRole);

            await _memberRepository.UpdateAsync(member, cancellationToken);

            return Result.Ok(member.UserId.Value);
        }
    }
}