using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.ManagementUsers;

namespace TaskManager.Modules.Management.Infrastructure.Database.Configurations;

public class ManagementUserConfiguration : IEntityTypeConfiguration<ManagementUser>
{
    public void Configure(EntityTypeBuilder<ManagementUser> builder)
    {
        builder.ToTable("ManagementUser");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedNever();

        builder.Property(x => x.FullName)
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired();
    }
}