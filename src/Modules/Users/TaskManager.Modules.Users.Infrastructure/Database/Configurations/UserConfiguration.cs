using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Modules.Users.Domain.Users.Entities;
using TaskManager.Modules.Users.Domain.Users.ValueObjects;

namespace TaskManager.Modules.Users.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x));

        builder.Property(x => x.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(100)
            .IsRequired();
    }
}