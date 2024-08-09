using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Infrastructure.Database.Configurations;

public class SubTaskItemConfiguration : IEntityTypeConfiguration<SubTaskItem>
{
    public void Configure(EntityTypeBuilder<SubTaskItem> builder)
    {
        builder.ToTable("SubTaskItems");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
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

        builder.Property(x => x.Progress)
            .HasConversion<string>()
            .IsRequired();
    }
}