namespace app.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("grade")]
    public partial class grade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public grade()
        {
            student_has_grade = new HashSet<student_has_grade>();
        }

        [Key]
        public int grade_id { get; set; }

        public int subject_id { get; set; }

        [Column("grade")]
        public int? grade1 { get; set; }

        public virtual subject subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_has_grade> student_has_grade { get; set; }
    }
}
