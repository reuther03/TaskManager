using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Database.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => TeamId.From(x))
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .HasConversion(x => x.Value, x => new Name(x))
            .IsRequired();

        builder.OwnsMany(x => x.UserIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("TeamId");
            ownedBuilder.ToTable("TeamManagementUserIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("UserId");

            builder.Metadata.FindNavigation(nameof(Team.UserIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.OwnsMany(x => x.TaskItemIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("TeamId");
            ownedBuilder.ToTable("TeamTaskIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("TaskItemId"); // Ensure this matches the database column

            builder.Metadata.FindNavigation(nameof(Team.TaskItemIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}