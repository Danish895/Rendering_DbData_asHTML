using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentAPI.Models;

namespace StudentAPI.DataAccessLayer.Context
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
        {
        }
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }
        public virtual DbSet<StudentDetail> StudentDetails { get; set; } = null!;
        //public virtual DbSet<GenericModel> GenericModels { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=Student;User Id=sa;Password=Youtube2021");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentDetail>(entity =>
            {
                entity.ToTable("StudentDetail");

                entity.Property(e => e.IsCreatedAt).HasColumnName("isCreatedAt");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

