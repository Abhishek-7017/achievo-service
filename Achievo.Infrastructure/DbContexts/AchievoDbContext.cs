using System;
using Achievo.Infrastructure.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Achievo.Infrastructure.DbContexts;

public class AchievoDbContext(DbContextOptions<AchievoDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskTemplate> TaskTemplates { get; set; }
    public DbSet<UserTask> UserTasks { get; set; }
}
