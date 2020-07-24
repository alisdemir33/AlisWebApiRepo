using System;
using System.Collections.Generic;

namespace JWTSample.ContosoModels
{
    public partial class CourseInstructor
    {
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
