using System;
using Achievo.Infrastructure.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Achievo.Infrastructure.DbContexts;

public class AchievoDbContext(DbContextOptions<AchievoDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskTemplate> TaskTemplates { get; set; }
    public DbSet<UserTask> UserTasks { get; set; }
    public DbSet<UserPoints> UserPoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTask>(entity =>
        {
            // Configure Owned Type: AssignedUser
            entity.OwnsOne(t => t.AssignedUser, a =>
            {
                a.Property(p => p.Id)
                    .HasColumnName(nameof(UserTask.AssignedUserId))
                    .IsRequired();

                a.Property(p => p.Name)
                    .HasColumnName(nameof(UserTask.AssignedUserName));
            });

            // Configure Owned Type: TaskTemplate
            entity.OwnsOne(t => t.TaskTemplate, t =>
            {
                t.Property(p => p.Id)
                    .HasColumnName("TaskTemplateId")
                    .IsRequired();

                t.Property(p => p.Name)
                    .HasColumnName("TaskTemplateName");
            });
        });

        // Indexes on scalar properties
        modelBuilder.Entity<UserTask>()
            .HasIndex(t => new { t.AssignedUserId, t.Status, t.AssignedAt })
            .HasDatabaseName("Ind_AssignedUserId_Status_AssignedAt");

        modelBuilder.Entity<UserTask>()
            .HasIndex(t => t.DueAt)
            .HasDatabaseName("Ind_DueAt");

        modelBuilder.Entity<UserPoints>()
            .HasIndex(t => new { t.UserId, t.CreatedAt })
            .HasDatabaseName("Ind_UserId_CreatedAt");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Id)
            .IsUnique()
            .HasDatabaseName("Ind_UserId");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.TotalPoints)
            .HasDatabaseName("Ind_UserPoint");
    }

}
