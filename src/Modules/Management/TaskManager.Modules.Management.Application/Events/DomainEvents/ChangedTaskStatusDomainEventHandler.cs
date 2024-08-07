using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Events.DomainEvents;
using TaskManager.Abstractions.Kernel.Events;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Events.DomainEvents;

public class ChangedTaskStatusDomainEventHandler : IDomainEventHandler<ChangedTaskStatusDomainEvent>
{
    private readonly IManagementsDbContext _managementsDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public ChangedTaskStatusDomainEventHandler(IManagementsDbContext managementsDbContext, IUnitOfWork unitOfWork)
    {
        _managementsDbContext = managementsDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ChangedTaskStatusDomainEvent notification, CancellationToken cancellationToken)
    {
        var targetTaskId = TaskItemId.From(notification.TaskId);
        var team = await _managementsDbContext.Teams
            .FirstOrDefaultAsync(x => x.TaskItemIds.Select(y => y.Value).Contains(targetTaskId.Value), cancellationToken);

        if (team == null)
            return;

        var totalTasks = team.TaskItemIds.Count;
        var completedTasks = await _managementsDbContext.Tasks
            .Where(x => team.TaskItemIds.Select(y => y.Value).Contains(x.Id) && x.Progress == TaskProgress.Completed).CountAsync(cancellationToken);

        var progress = totalTasks == 0 ? 0 : (double)completedTasks / totalTasks * 100;

        if (Math.Abs(progress - team.Progress) < 0.01)
            return;

        progress = Math.Round(progress, 2);
        team.SetProgress(progress);
        await _managementsDbContext.SaveChangesAsync(cancellationToken);
    }
}