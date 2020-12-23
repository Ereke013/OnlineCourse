using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCourse.Models
{
    public class CourseDep
    {
        public List<Department> depData { set; get; }
        public List<Cours> courseData { set; get; }

        public CourseDep(List<Department> depData, List<Cours> courseData)
        {
            this.depData = depData;
            this.courseData = courseData;
        }

        public CourseDep()
        {
        }
    }
}