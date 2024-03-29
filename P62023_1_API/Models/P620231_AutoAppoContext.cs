﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P62023_1_API.Models
{
    public partial class P620231_AutoAppoContext : DbContext
    {
        public P620231_AutoAppoContext()
        {
        }

        public P620231_AutoAppoContext(DbContextOptions<P620231_AutoAppoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; } = null!;
        public virtual DbSet<RecoveryCode> RecoveryCodes { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public virtual DbSet<View1> View1s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("SERVER=LAPTOP-0OCE7TFC\\SQLEXPRESS;DATABASE=P620231_AutoAppo; INTEGRATED SECURITY=TRUE;User Id=;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.AppoDate).HasColumnType("date");

                entity.Property(e => e.AppoStatusId).HasColumnName("AppoStatusID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Notes)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.AppoStatus)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AppoStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAppointmen475052");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAppointmen559984");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAppointmen691060");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAppointmen160080");
            });

            modelBuilder.Entity<AppointmentStatus>(entity =>
            {
                entity.HasKey(e => e.AppoStatusId)
                    .HasName("PK__Appointm__1B2A53DB655E81B8");

                entity.ToTable("Appointment Status");

                entity.Property(e => e.AppoStatusId).HasColumnName("AppoStatusID");

                entity.Property(e => e.AppoStatusDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RecoveryCode>(entity =>
            {
                entity.ToTable("RecoveryCode");

                entity.Property(e => e.RecoveryCodeId).HasColumnName("RecoveryCodeID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.GenerateDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecoveryCode1)
                    .HasMaxLength(6)
                    .HasColumnName("RecoveryCode")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.Notes)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.PromoDay).HasDefaultValueSql("('0')");

                entity.Property(e => e.ScheduleDateEnd).HasColumnType("date");

                entity.Property(e => e.ScheduleDateStart).HasColumnType("date");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceEstimatedCost).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CardId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CardID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoginPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser854768");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUser943402");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.UserRoleDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus");

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

                entity.Property(e => e.UserStatuDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_1");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CardId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CardID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoginPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserStatuDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
