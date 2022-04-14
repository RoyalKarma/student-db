namespace app.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class study_program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public study_program()
        {
            student_has_study_program = new HashSet<student_has_study_program>();
            study_program_has_subject = new HashSet<study_program_has_subject>();
        }

        [Key]
        public int program_id { get; set; }

        [Required]
        [StringLength(45)]
        public string program_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_has_study_program> student_has_study_program { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<study_program_has_subject> study_program_has_subject { get; set; }
    }
}
