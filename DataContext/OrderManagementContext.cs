﻿#nullable disable
using Microsoft.EntityFrameworkCore;

using OrderManagementMVC.Models;

namespace OrderManagementMVC.DataContext
{
	public partial class OrderManagementContext : DbContext
	{
		public OrderManagementContext()
		{
		}

		public OrderManagementContext( DbContextOptions<OrderManagementContext> options )
			 : base(options)
		{
		}

		public virtual DbSet<LabelsModel> Labels { get; set; }
		public virtual DbSet<OrderLabelsModel> OrderLabels { get; set; }
		public virtual DbSet<OrderTraceModel> OrderTrace { get; set; }
		public virtual DbSet<OrdersModel> Orders { get; set; }

		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=RAZVAN-OMEN;Initial Catalog=OrderManagement;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
			}
		}

		protected override void OnModelCreating( ModelBuilder modelBuilder )
		{
			modelBuilder.Entity<LabelsModel>(entity =>
			{
				entity.HasKey(e => e.LabelId);

				entity.Property(e => e.LabelId).HasColumnName("LabelID");

				entity.Property(e => e.LabelName)
						 .IsRequired()
						 .HasMaxLength(10);
			});

			modelBuilder.Entity<OrderLabelsModel>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.IdBoxNumber)
						 .IsRequired()
						 .HasMaxLength(50);

				entity.HasOne(d => d.OrderNumberNavigation)
						 .WithMany(p => p.OrderLabels)
						 .HasForeignKey(d => d.OrderNumber)
						 .OnDelete(DeleteBehavior.ClientSetNull)
						 .HasConstraintName("FK_OrderLabels_Orders");
			});

			modelBuilder.Entity<OrderTraceModel>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.DateOut).HasColumnType("datetime");

				entity.Property(e => e.IdBoxNumber)
						 .IsRequired()
						 .HasMaxLength(50);

				entity.Property(e => e.MachineId).HasMaxLength(5);
			});

			modelBuilder.Entity<OrdersModel>(entity =>
			{
				entity.HasKey(e => e.OrderNumber);

				entity.Property(e => e.OrderNumber).ValueGeneratedNever();

				entity.Property(e => e.Client)
						 .IsRequired()
						 .HasMaxLength(50);

				entity.Property(e => e.CustomSortField).HasMaxLength(50);

				entity.Property(e => e.DateFinished).HasColumnType("datetime");

				entity.Property(e => e.DateInProduction).HasColumnType("datetime");

				entity.Property(e => e.DateInSystem).HasColumnType("datetime");

				entity.Property(e => e.DocumentFormat)
						 .IsRequired()
						 .HasMaxLength(5);

				entity.Property(e => e.DocumentName)
						 .IsRequired()
						 .HasMaxLength(20);

				entity.Property(e => e.EnvelopeType)
						 .IsRequired()
						 .HasMaxLength(5);

				entity.Property(e => e.LabelType)
						 .IsRequired()
						 .HasMaxLength(10);

				entity.Property(e => e.OrderStatus)
						 .IsRequired()
						 .HasMaxLength(20);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial( ModelBuilder modelBuilder );
	}
}