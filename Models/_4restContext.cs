using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TimeShareProject.Models;

public partial class _4restContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaims, ApplicationUserRoles, ApplicationUserLogins, ApplicationRoleClaims, ApplicationUserTokens>
{
    public _4restContext()
    {
    }

    public _4restContext(DbContextOptions<_4restContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Block> Blocks { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<New> News { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Property> Properties { get; set; }
    public virtual DbSet<Rate> Rates { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    //public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
    //public virtual DbSet<ApplicationUserRoles> ApplicationUserRoles { get; set; }
    //public virtual DbSet<ApplicationUserLogins> ApplicationUserLogin { get; set; }
    //public virtual DbSet<ApplicationUserTokens> ApplicationUserToken { get; set; }
    //public virtual DbSet<ApplicationUserClaims> ApplicationUserClaim { get; set; }
    //public virtual DbSet<ApplicationRoleClaims> ApplicationRoleClaim { get; set; }
    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            string tableName = entityType.GetTableName() ?? "";
            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
