using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Calendar.Models;


namespace Calendar.Data
{
    //Upgrade dotnet?.?
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Upgrade dotnet?.?
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        //}

        public DbSet<Event> Event { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<TeamProject> TeamProject { get; set; }

        public DbSet<Acknowledgement> Acknowledgement { get; set; }

        public DbSet<Attachment> Attachment { get; set; }
    }
}
