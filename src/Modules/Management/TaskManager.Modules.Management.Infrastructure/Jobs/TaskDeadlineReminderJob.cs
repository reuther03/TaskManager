using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Abstractions.Email;
using TaskManager.Modules.Management.Infrastructure.Database;

namespace TaskManager.Modules.Management.Infrastructure.Jobs;

public class TaskDeadlineReminderJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TaskDeadlineReminderJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // using var scope = _serviceScopeFactory.CreateScope();
        // var context = scope.ServiceProvider.GetRequiredService<ManagementsDbContext>();
        // var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
        //
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     var tasks = await context.Tasks
        //             // 7 days before deadline
        //             .Where(x => x.Deadline. == DateTime.UtcNow.Date.AddDays(7));
        //         .ToListAsync(stoppingToken);
        //
        //     var taskUsers = await context.TeamMembers
        //         .Where(x => tasks.Select(y => y.AssignedUserId).Contains(x.UserId))
        //         .ToListAsync(stoppingToken);
        //
        //     var taskByUser = tasks.Join(taskUsers, x => x.AssignedUserId, y => y.UserId, (x, y) => new { Task = x, User = y });
        //
        //     foreach (var userTask in taskByUser)
        //     {
        //         userTask.
        //     }
        //
        //     // await Task.Delay(TimeSpan.FromMinutes(120), stoppingToken);
        //     await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        // }
        return Task.CompletedTask;
    }
}