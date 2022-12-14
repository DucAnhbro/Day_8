using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Day_8.Models;

public partial class EmployessContext : DbContext
{
    public EmployessContext()
    {
    }

    public EmployessContext(DbContextOptions<EmployessContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<EmployeRole> EmployeRoles { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0LJAOF9;Database=Employess; Trust Server Certificate=true;User Id=sa;Password=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Information");

            entity.ToTable("Employe");

            entity.Property(e => e.Adress).HasMaxLength(250);
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.IsSystem).HasColumnName("isSystem");
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.UserName).HasMaxLength(250);
        });

        modelBuilder.Entity<EmployeRole>(entity =>
        {
            entity.ToTable("EmployeRole");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.RoleName).HasMaxLength(250);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Action).HasMaxLength(150);
            entity.Property(e => e.Controller).HasMaxLength(150);
            entity.Property(e => e.RoleName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
