using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebA.Models;

public partial class NestStudentContext : DbContext
{
    public NestStudentContext()
    {
    }

    public NestStudentContext(DbContextOptions<NestStudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AskForLeave> AskForLeaves { get; set; }

    public virtual DbSet<Fix> Fixes { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<RollcallStudent> RollcallStudents { get; set; }

    public virtual DbSet<Server> Servers { get; set; }

    public virtual DbSet<Sue> Sues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-T6R9S01R\\CHIAO;Initial Catalog=NestStudent;User ID=chiao;Password=5470705sehun;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.ToTable("Announcement");
        });

        modelBuilder.Entity<AskForLeave>(entity =>
        {
            entity.ToTable("AskForLeave");

            entity.Property(e => e.Reason)
                .HasMaxLength(99)
                .IsFixedLength();
            entity.Property(e => e.SelectedOption)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
        });

        modelBuilder.Entity<Fix>(entity =>
        {
            entity.ToTable("fix");

            entity.Property(e => e.Site).HasColumnName("site");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.ToTable("pay");
        });

        modelBuilder.Entity<RollcallStudent>(entity =>
        {
            entity.HasKey(e => e.RollcallId).HasName("PK__Rollcall__67BDB6E3013E9FC2");

            entity.ToTable("RollcallStudent");

            entity.HasIndex(e => new { e.StudentId, e.Datetime }, "UQ_Rollcall_StudentID_Datetime").IsUnique();

            entity.Property(e => e.RollcallId).HasColumnName("Rollcall_ID");
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Rollcall).HasMaxLength(50);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
        });

        modelBuilder.Entity<Server>(entity =>
        {
            entity.ToTable("Server");

            entity.Property(e => e.Idnumber).HasColumnName("IDNumber");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
        });

        modelBuilder.Entity<Sue>(entity =>
        {
            entity.ToTable("sue");

            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.Roomnumber).HasColumnName("roomnumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
