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
        [Key]
        [Column(Order = 0)]
        public int grade_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int student_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int subject_id { get; set; }

        [Column("grade")]
        public int? grade1 { get; set; }

        public virtual student student { get; set; }

        public virtual subject subject { get; set; }
    }
}
