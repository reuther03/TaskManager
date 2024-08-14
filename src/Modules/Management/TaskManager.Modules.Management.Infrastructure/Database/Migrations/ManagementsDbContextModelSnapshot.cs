﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManager.Modules.Management.Infrastructure.Database;

#nullable disable

namespace TaskManager.Modules.Management.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ManagementsDbContext))]
    partial class ManagementsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("management")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.ManagementUsers.ManagementUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("ManagementUser", "management");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TaskItems.SubTaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Progress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TaskItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("TaskItemId");

                    b.ToTable("SubTaskItems", "management");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TaskItems.TaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssignedUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("Priority")
                        .HasColumnType("boolean");

                    b.Property<string>("Progress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ReminderSent")
                        .HasColumnType("boolean");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("TaskItems", "management");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TeamFiles.TeamFile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamFile", "management");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TeamMembers.TeamMember", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("TeamRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMembers", "management");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.Teams.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("CompletedTasks")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<double>("Progress")
                        .HasPrecision(5, 2)
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Teams", "management");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TaskItems.SubTaskItem", b =>
                {
                    b.HasOne("TaskManager.Modules.Management.Domain.TaskItems.TaskItem", null)
                        .WithMany("SubTaskItems")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TeamFiles.TeamFile", b =>
                {
                    b.HasOne("TaskManager.Modules.Management.Domain.Teams.Team", null)
                        .WithMany("TeamFiles")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TeamMembers.TeamMember", b =>
                {
                    b.HasOne("TaskManager.Modules.Management.Domain.Teams.Team", null)
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.Teams.Team", b =>
                {
                    b.OwnsMany("TaskManager.Modules.Management.Domain.TaskItems.TaskItemId", "TaskItemIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("TeamId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("TaskItemId");

                            b1.HasKey("Id");

                            b1.HasIndex("TeamId");

                            b1.ToTable("TeamTaskIds", "management");

                            b1.WithOwner()
                                .HasForeignKey("TeamId");
                        });

                    b.Navigation("TaskItemIds");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.TaskItems.TaskItem", b =>
                {
                    b.Navigation("SubTaskItems");
                });

            modelBuilder.Entity("TaskManager.Modules.Management.Domain.Teams.Team", b =>
                {
                    b.Navigation("TeamFiles");

                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
