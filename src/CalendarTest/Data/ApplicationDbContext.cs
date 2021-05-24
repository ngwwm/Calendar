using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CalendarTest.Models;

#nullable disable

namespace CalendarTest.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acknowledgement> Acknowledgements { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamProject> TeamProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.242.11;Database=eventdb;User Id=sa;Password=Welcome123;Trusted_Connection=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Acknowledgement>(entity =>
            {
                entity.ToTable("Acknowledgement");

                entity.HasIndex(e => e.EventId, "IX_Acknowledgement_EventID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AckMessage).HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedByDisplayName).HasMaxLength(50);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.Team)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedByDisplayName).HasMaxLength(50);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Acknowledgements)
                    .HasForeignKey(d => d.EventId);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedByDisplayName).HasMaxLength(50);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedByDisplayName).HasMaxLength(50);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionBy).HasMaxLength(100);

                entity.Property(e => e.AffectedHosts)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.AffectedProjects)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.AffectedTeams)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedByDisplayName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Environment)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.EventStatus).HasMaxLength(5);

                entity.Property(e => e.FallbackProcedure).HasMaxLength(1000);

                entity.Property(e => e.HealthCheckBy).HasMaxLength(100);

                entity.Property(e => e.Impact)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ImpactAnalysis).HasMaxLength(1000);

                entity.Property(e => e.Likelihood)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.MaintProcedure).HasMaxLength(1000);

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.Result).HasMaxLength(100);

                entity.Property(e => e.RiskAnalysis).HasMaxLength(1000);

                entity.Property(e => e.RiskLevel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TaskDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedByDisplayName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.VerificationStep).HasMaxLength(1000);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Administrator)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CalendarStyle).HasMaxLength(15);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CalendarStyle).HasMaxLength(15);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<TeamProject>(entity =>
            {
                entity.ToTable("TeamProject");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Team)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
