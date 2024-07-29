using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(TaskItemId taskItemId, CancellationToken cancellationToken);
    Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(TaskItem task, CancellationToken cancellationToken);
    Task Update(TaskItem task, CancellationToken cancellationToken);
}