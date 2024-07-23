using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Infrastructure.Database.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new TaskItemId(x))
            .ValueGeneratedNever();

        builder.Property(x => x.TaskName)
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .HasConversion(x => x.Value, x => new Description(x))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Deadline)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.Progress)
            .IsRequired();
    }
}