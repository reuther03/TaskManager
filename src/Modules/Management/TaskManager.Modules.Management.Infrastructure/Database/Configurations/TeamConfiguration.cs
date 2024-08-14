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

        builder.Property(x => x.Progress)
            .HasPrecision(5, 2)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.OwnsMany(x => x.TaskItemIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("TeamId");
            ownedBuilder.ToTable("TeamTaskIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("TaskItemId");

            builder.Metadata
                .FindNavigation(nameof(Team.TaskItemIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.HasMany(x => x.TeamMembers)
            .WithOne()
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.TeamFiles)
            .WithOne()
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}