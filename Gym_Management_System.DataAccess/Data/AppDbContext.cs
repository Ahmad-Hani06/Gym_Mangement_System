using System;
using System.Collections.Generic;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasIndex(e => e.PersonId, "IX_Coaches").IsUnique();

            entity.Property(e => e.CoachId).HasColumnName("CoachID");
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithOne(p => p.Coach)
                .HasForeignKey<Coach>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coaches_Persons");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasIndex(e => e.PersonId, "IX_Members").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");

            entity.HasOne(d => d.Person).WithOne(p => p.Member)
                .HasForeignKey<Member>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Members_Persons");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.Property(e => e.MembershipId).HasColumnName("MembershipID");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())", "DF_Memberships_StartDate");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.SubscriptionTypeId).HasColumnName("SubscriptionTypeID");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memberships_Users");

            entity.HasOne(d => d.Member).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memberships_Members");

            entity.HasOne(d => d.SubscriptionType).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Memberships_SubscriptionTypes");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.MembershipId, "IX_Payments").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserID");
            entity.Property(e => e.MembershipId).HasColumnName("MembershipID");
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(sysdatetime())", "DF_Payments_PaymentDate");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Users");

            entity.HasOne(d => d.Membership).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.MembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Memberships");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<SubscriptionType>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId);

            entity.HasIndex(e => e.Name, "IX_SubscriptionTypes").IsUnique();

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.PersonId, "IX_Users").IsUnique();

            entity.HasIndex(e => e.UserName, "IX_Users_1").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Persons");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
