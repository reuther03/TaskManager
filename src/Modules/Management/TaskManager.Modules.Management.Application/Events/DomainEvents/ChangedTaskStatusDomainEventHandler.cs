﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Events.DomainEvents;
using TaskManager.Abstractions.Kernel.Events;
using TaskManager.Modules.Management.Application.Database.Abstractions;

namespace TaskManager.Modules.Management.Application.Events.DomainEvents;

public class ChangedTaskStatusDomainEventHandler : IDomainEventHandler<TaskItemCompletedDomainEvent>
{
    private readonly IManagementsDbContext _managementsDbContext;
    public ChangedTaskStatusDomainEventHandler(IManagementsDbContext managementsDbContext)
    {
        _managementsDbContext = managementsDbContext;
    }

    public async Task Handle(TaskItemCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var team = await _managementsDbContext.Teams
            .Where(x => x.TaskItemIds.Select(y => y.Value).Contains(notification.TaskId))
            .FirstOrDefaultAsync(cancellationToken);

        if (team == null)
            return;

        team.IncrementCompletedTasks(notification.TaskId);
        await _managementsDbContext.SaveChangesAsync(cancellationToken);
    }
}