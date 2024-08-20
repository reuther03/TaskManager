using MediatR;
using TaskManager.Abstractions.Events;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;

namespace TaskManager.Modules.Management.Application.Features.Events;

public class DeleteUserHandler : INotificationHandler<UserDeletedEvent>
{
    private readonly IManagementUserRepository _userRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IManagementUserRepository userRepository, IUnitOfWork unitOfWork, ITeamMemberRepository teamMemberRepository,
        ITeamRepository teamRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _teamMemberRepository = teamMemberRepository;
        _teamRepository = teamRepository;
    }

    public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);
        if (user is null)
            return;

        var teamMembers = await _teamMemberRepository.GetAllByIdAsync(notification.UserId, cancellationToken);
        if (teamMembers is null)
            return;

        foreach (var teamMember in teamMembers)
        {
            var team = await _teamRepository.GetByIdAsync(teamMember.TeamId, cancellationToken);
            team?.RemoveMember(teamMember);
            if (team is not null && team.TeamMembers.Count == 0)
            {
                _teamRepository.Remove(team);
            }
        }

        _userRepository.Remove(user);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}