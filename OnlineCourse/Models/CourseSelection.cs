namespace OnlineCourse.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourseSelection
    {
        public int Id { get; set; }

        public int PaymentId { get; set; }

        public bool? Check { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
