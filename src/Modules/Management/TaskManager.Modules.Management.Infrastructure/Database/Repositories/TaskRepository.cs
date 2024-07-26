using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TaskRepository : ITaskRepository
{
    private readonly ManagementsDbContext _context;

    public TaskRepository(ManagementsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken)
     => await _context.AddAsync(task, cancellationToken);
}