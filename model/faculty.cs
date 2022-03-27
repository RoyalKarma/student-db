namespace app.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("faculty")]
    public partial class faculty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public faculty()
        {
            study_program_has_faculty = new HashSet<study_program_has_faculty>();
        }

        [Key]
        public int faculty_id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<study_program_has_faculty> study_program_has_faculty { get; set; }
    }
}
