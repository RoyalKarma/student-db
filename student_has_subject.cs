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
    using System.Collections.Generic;
    
    public partial class student_has_subject
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public int subject_id { get; set; }
    
        public virtual student student { get; set; }
        public virtual subject subject { get; set; }
    }
}
