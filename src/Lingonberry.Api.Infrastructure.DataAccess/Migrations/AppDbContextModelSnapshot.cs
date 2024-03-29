﻿// <auto-generated />
using System;
using Lingonberry.Api.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lingonberry.Api.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DepartmentDivision", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("integer");

                    b.Property<int>("DivisionsId")
                        .HasColumnType("integer");

                    b.HasKey("DepartmentsId", "DivisionsId");

                    b.HasIndex("DivisionsId");

                    b.ToTable("DepartmentDivision", (string)null);
                });

            modelBuilder.Entity("DepartmentGroup", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupsId")
                        .HasColumnType("integer");

                    b.HasKey("DepartmentsId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("DepartmentGroup", (string)null);
                });

            modelBuilder.Entity("DepartmentLocation", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("integer");

                    b.Property<int>("LocationsId")
                        .HasColumnType("integer");

                    b.HasKey("DepartmentsId", "LocationsId");

                    b.HasIndex("LocationsId");

                    b.ToTable("DepartmentLocation", (string)null);
                });

            modelBuilder.Entity("DivisionGroup", b =>
                {
                    b.Property<int>("DivisionsId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupsId")
                        .HasColumnType("integer");

                    b.HasKey("DivisionsId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("DivisionGroup", (string)null);
                });

            modelBuilder.Entity("DivisionLocation", b =>
                {
                    b.Property<int>("DivisionsId")
                        .HasColumnType("integer");

                    b.Property<int>("LocationsId")
                        .HasColumnType("integer");

                    b.HasKey("DivisionsId", "LocationsId");

                    b.HasIndex("LocationsId");

                    b.ToTable("DivisionLocation", (string)null);
                });

            modelBuilder.Entity("GroupLocation", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("integer");

                    b.Property<int>("LocationsId")
                        .HasColumnType("integer");

                    b.HasKey("GroupsId", "LocationsId");

                    b.HasIndex("LocationsId");

                    b.ToTable("GroupLocation", (string)null);
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments", (string)null);
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Divisions", (string)null);
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Locations", (string)null);
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Users.AppIdentityRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("now() at time zone 'UTC'");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsVacancy")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("LastTokenResetAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("character varying(11)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Position")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<DateTime?>("RemovedAt")
                        .HasColumnType("timestamp")
                        .HasComment("For soft-deletes");

                    b.Property<int?>("Salary")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("now() at time zone 'UTC'");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .IsUnicode(false)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("UserNumber")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<int>("UserPosition")
                        .HasColumnType("integer");

                    b.Property<string>("UserPositionName")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<int>("WorkType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DivisionId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LocationId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("RemovedAt");

                    b.HasIndex(new[] { "Email" }, "Email");

                    b.HasIndex(new[] { "NormalizedEmail" }, "NormalizedEmail")
                        .IsUnique();

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Xml")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DepartmentDivision", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Locations.Division", null)
                        .WithMany()
                        .HasForeignKey("DivisionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DepartmentGroup", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Locations.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DepartmentLocation", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Locations.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DivisionGroup", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Division", null)
                        .WithMany()
                        .HasForeignKey("DivisionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Locations.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DivisionLocation", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Division", null)
                        .WithMany()
                        .HasForeignKey("DivisionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Locations.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupLocation", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Locations.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Users.User", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Locations.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lingonberry.Api.Domain.Locations.Division", "Division")
                        .WithMany("Users")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lingonberry.Api.Domain.Locations.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lingonberry.Api.Domain.Locations.Location", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Department");

                    b.Navigation("Division");

                    b.Navigation("Group");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Users.AppIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Users.AppIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lingonberry.Api.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Lingonberry.Api.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Division", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Group", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Lingonberry.Api.Domain.Locations.Location", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
