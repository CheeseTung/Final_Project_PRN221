﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Final_Project_PRN221.Models
{
    public partial class Project_PRN221Context : DbContext
    {
        public Project_PRN221Context()
        {
        }

        public Project_PRN221Context(DbContextOptions<Project_PRN221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Electricity> Electricities { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-SJ0MHFN2\\CHITUNG;uid=sa;pwd=1234567;database=Project_PRN221");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Electricity>(entity =>
            {
                entity.ToTable("Electricity");

                entity.Property(e => e.ElectricityId).HasColumnName("ElectricityID");

                entity.Property(e => e.Month).HasColumnType("date");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Electricities)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Electricity_Rooms");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.ToTable("Payment_Details");

                entity.Property(e => e.PaymentDetailId).HasColumnName("payment_detail_id");

                entity.Property(e => e.CleanMoney)
                    .HasColumnType("money")
                    .HasColumnName("cleanMoney");

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.DrinkWaterMoney)
                    .HasColumnType("money")
                    .HasColumnName("drinkWaterMoney");

                entity.Property(e => e.ElectricityId).HasColumnName("electricityId");

                entity.Property(e => e.NetworkMoney)
                    .HasColumnType("money")
                    .HasColumnName("networkMoney");

                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.Property(e => e.RoomCharge)
                    .HasColumnType("money")
                    .HasColumnName("room_charge");

                entity.Property(e => e.RoomId).HasColumnName("roomId");

                entity.Property(e => e.WaterMoney)
                    .HasColumnType("money")
                    .HasColumnName("waterMoney");

                entity.HasOne(d => d.Electricity)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.ElectricityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Details_Electricity");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Details_Payment");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.PaymentDetails)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Details_Rooms");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoomID");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_Table_1");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Rooms");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
