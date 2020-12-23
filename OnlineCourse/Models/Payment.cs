namespace OnlineCourse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            CourseSelections = new HashSet<CourseSelection>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int CourseId { get; set; }

        public int PriceId { get; set; }

        public int Course_Duration { get; set; }

        public DateTime DateOfPay { get; set; }

        public virtual Cours Cours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseSelection> CourseSelections { get; set; }

        public virtual Price Price { get; set; }

    }
}
