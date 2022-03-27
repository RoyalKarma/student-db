using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace app.model
{
    public partial class UniversityModel : DbContext
    {
        public UniversityModel()
            : base("name=UniversityModel")
        {
        }

        public virtual DbSet<faculty> faculties { get; set; }
        public virtual DbSet<grade> grades { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<student_has_study_program> student_has_study_program { get; set; }
        public virtual DbSet<student_has_subject> student_has_subject { get; set; }
        public virtual DbSet<study_program> study_program { get; set; }
        public virtual DbSet<study_program_has_faculty> study_program_has_faculty { get; set; }
        public virtual DbSet<study_program_has_subject> study_program_has_subject { get; set; }
        public virtual DbSet<subject> subjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<faculty>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<faculty>()
                .HasMany(e => e.study_program_has_faculty)
                .WithRequired(e => e.faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<student>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<student>()
                .HasMany(e => e.grades)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.student_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<student>()
                .HasMany(e => e.student_has_study_program)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.student_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<student>()
                .HasMany(e => e.student_has_subject)
                .WithRequired(e => e.student)
                .HasForeignKey(e => e.student_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<study_program>()
                .Property(e => e.program_name)
                .IsUnicode(false);

            modelBuilder.Entity<study_program>()
                .HasMany(e => e.student_has_study_program)
                .WithRequired(e => e.study_program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<study_program>()
                .HasMany(e => e.study_program_has_faculty)
                .WithRequired(e => e.study_program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<study_program>()
                .HasMany(e => e.study_program_has_subject)
                .WithRequired(e => e.study_program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subject>()
                .Property(e => e.subject_name)
                .IsUnicode(false);

            modelBuilder.Entity<subject>()
                .HasMany(e => e.grades)
                .WithRequired(e => e.subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subject>()
                .HasMany(e => e.student_has_subject)
                .WithRequired(e => e.subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subject>()
                .HasMany(e => e.study_program_has_subject)
                .WithRequired(e => e.subject)
                .WillCascadeOnDelete(false);
        }
    }
}
