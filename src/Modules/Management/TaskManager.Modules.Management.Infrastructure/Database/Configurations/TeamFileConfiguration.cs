using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Modules.Management.Domain.TeamFiles;

namespace TaskManager.Modules.Management.Infrastructure.Database.Configurations;

public class TeamFileConfiguration : IEntityTypeConfiguration<TeamFile>
{
    public void Configure(EntityTypeBuilder<TeamFile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FileUrl)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired();
    }
}