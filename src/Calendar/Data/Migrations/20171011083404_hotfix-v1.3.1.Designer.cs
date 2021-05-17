using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Calendar.Data;

namespace Calendar.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171011083404_hotfix-v1.3.1")]
    partial class hotfixv131
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Calendar.Models.Acknowledgement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AckMessage")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("CreatedByDisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("EventID");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UpdatedByDisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("ID");

                    b.HasIndex("EventID");

                    b.ToTable("Acknowledgement");
                });

            modelBuilder.Entity("Calendar.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Calendar.Models.Attachment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("CreatedByDisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("EventID");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UpdatedByDisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("ID");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Calendar.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionBy")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("AffectedHosts")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("AffectedProjects")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 300);

                    b.Property<string>("AffectedTeams")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("CreatedByDisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<string>("Environment")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("EventStatus")
                        .HasAnnotation("MaxLength", 5);

                    b.Property<string>("FallbackProcedure")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("HealthCheckBy")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Impact")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("ImpactAnalysis")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Likelihood")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("MaintProcedure")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Reference")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Result")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("RiskAnalysis")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("RiskLevel")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<DateTime>("StartDateTime");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UpdatedByDisplayName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("VerificationStep")
                        .HasAnnotation("MaxLength", 1000);

                    b.HasKey("ID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Calendar.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Administrator")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("CalendarStyle")
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.HasKey("ID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Calendar.Models.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CalendarStyle")
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("DomainGroup");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.HasKey("ID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Calendar.Models.TeamProject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Project")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.HasKey("ID");

                    b.ToTable("TeamProject");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Calendar.Models.Acknowledgement", b =>
                {
                    b.HasOne("Calendar.Models.Event", "Event")
                        .WithMany("Acknowledgements")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Calendar.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Calendar.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Calendar.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
