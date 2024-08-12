using Microsoft.EntityFrameworkCore;
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

    public async Task<TaskItem?> GetByIdAsync(TaskItemId taskItemId, CancellationToken cancellationToken)
        => await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskItemId, cancellationToken);

    public async Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
        => await _context.Tasks.ToListAsync(cancellationToken);

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken)
        => await _context.AddAsync(task, cancellationToken);

    public Task DeleteAsync(TaskItem task, CancellationToken cancellationToken)
    {
        _context.Tasks.Remove(task);
        return Task.CompletedTask;
    }
}