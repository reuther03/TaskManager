using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.Groups.Entities;
using TaskManager.Modules.Management.Domain.Groups.ValueObjects;

namespace TaskManager.Modules.Management.Infrastructure.Database.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
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
            ownedBuilder.ToTable("TeamUserIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("UserIds");

            builder.Metadata.FindNavigation(nameof(Team.UserIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.HasMany(x => x.Tasks)
            .WithOne()
            .HasForeignKey("TeamId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}