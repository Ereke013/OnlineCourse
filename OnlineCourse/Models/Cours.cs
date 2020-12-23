namespace OnlineCourse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string CourseName { get; set; }

        public int LevelId { get; set; }

        public int DepartmentId { get; set; }

        public int Course_Duration { get; set; }

        public virtual CourseLevel CourseLevel { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
