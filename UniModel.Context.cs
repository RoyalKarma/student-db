//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace app
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<faculty> faculties { get; set; }
        public virtual DbSet<grade> grades { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<student_has_faculty> student_has_faculty { get; set; }
        public virtual DbSet<student_has_grade> student_has_grade { get; set; }
        public virtual DbSet<student_has_subject> student_has_subject { get; set; }
        public virtual DbSet<subject> subjects { get; set; }
    }
}
