using System;
using System.Collections.Generic;

namespace JWTSample.ContosoModels
{
    public partial class Course
    {
        public Course()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            Enrollment = new HashSet<Enrollment>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public Guid? DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructor { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
