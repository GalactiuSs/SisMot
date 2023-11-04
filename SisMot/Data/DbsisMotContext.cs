using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SisMot.Models;

namespace SisMot.Data;

public partial class DbsisMotContext : DbContext
{
    public DbsisMotContext()
    {
    }

    public DbsisMotContext(DbContextOptions<DbsisMotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Motel> Motels { get; set; }

    public virtual DbSet<MotelPhoto> MotelPhotos { get; set; }

    public virtual DbSet<MotelService> MotelServices { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonPhoto> PersonPhotos { get; set; }

    public virtual DbSet<PersonRequest> PersonRequests { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Motel>(entity =>
        {
            entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Person).WithMany(p => p.Motels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Motel_Person");
        });

        modelBuilder.Entity<MotelPhoto>(entity =>
        {
            entity.HasOne(d => d.Motel).WithMany(p => p.MotelPhotos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MotelPhoto_Motel");
        });

        modelBuilder.Entity<MotelService>(entity =>
        {
            entity.HasOne(d => d.Motel).WithMany(p => p.MotelServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MotelServices_Motel");

            entity.HasOne(d => d.Services).WithMany(p => p.MotelServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MotelServices_Services");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.LastUpdate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<PersonPhoto>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PersonPhoto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonPhoto_Person");
        });

        modelBuilder.Entity<PersonRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OwnerRequest");

            entity.Property(e => e.StatusRequest).HasComment("APROBADO\r\nPENDIENTE\r\nRECHAZADO");

            entity.HasOne(d => d.Motel).WithMany(p => p.PersonRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonRequest_Motel");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PersonRequest_Person");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.RegisterDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Person");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
