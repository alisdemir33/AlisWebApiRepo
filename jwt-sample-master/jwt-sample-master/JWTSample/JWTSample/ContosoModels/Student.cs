using System;
using System.Collections.Generic;

namespace JWTSample.ContosoModels
{
    public partial class Student
    {
        public Student()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Explanation { get; set; }

        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
