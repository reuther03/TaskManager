// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using TaskManager.Modules.Management.Domain.TaskItems;
// using TaskManager.Modules.Management.Infrastructure.Database;
//
// namespace TaskManager.Modules.Management.Infrastructure.Jobs;
//
// public class TeamProgressJob : BackgroundService
// {
//     private readonly IServiceScopeFactory _serviceScopeFactory;
//
//     public TeamProgressJob(IServiceScopeFactory serviceScopeFactory)
//     {
//         _serviceScopeFactory = serviceScopeFactory;
//     }
//
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         using var scope = _serviceScopeFactory.CreateScope();
//         var context = scope.ServiceProvider.GetRequiredService<ManagementsDbContext>();
//
//
//         while (!stoppingToken.IsCancellationRequested)
//         {
//             var teams = await context.Teams
//                 .ToListAsync(stoppingToken);
//
//             foreach (var team in teams)
//             {
//                 var totalTasks = team.TaskItemIds.Count;
//                 var completedTasks = await context.Tasks
//                     .Where(x => team.TaskItemIds.Contains(x.Id) && x.Progress == TaskProgress.Completed)
//                     .CountAsync(stoppingToken);
//
//                 var progress = totalTasks == 0 ? 0 : (double)completedTasks / totalTasks * 100;
//                 if (Math.Abs(progress - team.Progress) > 0.001)
//                     team.IncrementCompletedTasks(progress);
//             }
//
//             await context.SaveChangesAsync(stoppingToken);
//             await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
//         }
//     }
// }