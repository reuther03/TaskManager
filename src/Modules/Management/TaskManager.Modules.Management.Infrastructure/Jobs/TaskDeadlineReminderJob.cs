using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Abstractions.Email;
using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Infrastructure.Database;

namespace TaskManager.Modules.Management.Infrastructure.Jobs;

public class TaskDeadlineReminderJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TaskDeadlineReminderJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ManagementsDbContext>();
        var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var tasks = await context.Tasks
                .Where(x => x.Deadline.Date >= DateTime.Today && x.Deadline.Date <= DateTime.Today.AddDays(7) && !x.ReminderSent)
                .ToListAsync(stoppingToken);

            var taskUsers = await context.TeamMembers
                .Where(x => tasks.Select(y => y.AssignedUserId).Contains(x.UserId))
                .ToListAsync(stoppingToken);

            var taskByUser = tasks.Join(taskUsers, x => x.AssignedUserId, y => y.UserId, (x, y) => new { Task = x, User = y });


            foreach (var userTask in taskByUser)
            {
                var userEmail = await context.Users
                    .Where(x => x.Id == userTask.User.UserId)
                    .Select(x => x.Email)
                    .FirstOrDefaultAsync(cancellationToken: stoppingToken);

                var email = new EmailMessage(userEmail,
                    "Task deadline reminder",
                    $"""
                     <div style="background-color: #f0f0f0; padding: 20px; font-family: Arial, sans-serif; line-height: 1.6;">
                         <div style="max-width: 600px; margin: auto; background: #ffffff; padding: 20px; border-radius: 8px;">
                             <h2 style="color: #333333; text-align: center;">Upcoming Deadline Alert!</h2>
                             <p style="color: #555555;">Hello {userTask.Task.TaskName},</p>
                             <p style="color: #555555;">This is a reminder that the deadline for your task "<strong>{userTask.Task.TaskName}</strong>" is approaching. Please make sure to complete it on time.</p>
                             <p style="color: #555555;">Deadline: <strong>{userTask.Task.Deadline}</strong></p>
                             <p style="color: #555555;">If you have any questions or need assistance, please reach out to our support team.</p>
                             <div style="text-align: center; margin-top: 20px;">
                                 <a href="https://support.yourapp.com" style="display: inline-block; background-color: #007bff; color: #ffffff; padding: 10px 20px; border-radius: 4px; text-decoration: none;">Contact Support</a>
                             </div>
                         </div>
                     </div>
                     """);

                await emailSender.Send(email);
                userTask.Task.ChangeReminderSent();
            }

            var delayedTasks = await context.Tasks
                .Where(x => x.Deadline.Date < x.CreatedAt && x.Progress != TaskProgress.Completed)
                .ToListAsync(stoppingToken);

            foreach (var delayedTask in delayedTasks)
            {
                delayedTask.ChangeStatus(TaskProgress.Delayed);
            }

            await context.SaveChangesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}