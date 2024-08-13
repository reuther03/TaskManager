using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record DeleteTeamCommand(Guid TeamId) : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserService _userService;
        private readonly ITeamMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            ITeamRepository teamRepository,
            IUserService userService,
            ITeamMemberRepository memberRepository,
            IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _userService = userService;
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result.BadRequest("User not found");

            var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken);
            if (team is null)
                return Result.NotFound("Task not found");

            if (!await _memberRepository.MemberInTeamAsync(user.UserId, team.Id, cancellationToken))
                return Result.BadRequest("You are not a member of this team");

            _teamRepository.Remove(team);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}