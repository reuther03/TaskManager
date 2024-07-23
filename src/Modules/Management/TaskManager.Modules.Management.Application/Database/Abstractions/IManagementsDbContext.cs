﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Domain.ManagementUsers;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Database.Abstractions;

public interface IManagementsDbContext
{
    DbSet<Team> Teams { get; }
    DbSet<TaskItem> Tasks { get; }
    DbSet<ManagementUser> Users { get; }
}