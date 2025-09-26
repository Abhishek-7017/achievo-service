using System;
using Achievo.Infrastructure.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Achievo.Infrastructure.DbContexts;

public class UserDbContext(DbContextOptions<UserDbContext> options):DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
