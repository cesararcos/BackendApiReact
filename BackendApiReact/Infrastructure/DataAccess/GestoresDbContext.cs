using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BackendApiReact.Domain.Entities;

namespace BackendApiReact.Infrastructure.DataAccess
{
    public partial class GestoresDbContext : DbContext
    {
        public GestoresDbContext()
        {
        }

        public GestoresDbContext(DbContextOptions<GestoresDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Roles> Roles { get; set; } = null!;
        public virtual DbSet<States> States { get; set; } = null!;
        public virtual DbSet<Tasks> Tasks { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK_Users_States");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_Users_Tasks");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
