using System;
using System.Collections.Generic;

namespace JWTSample.ContosoModels
{
    public partial class Enrollment
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public int? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Student { get; set; }
        public virtual Student StudentNavigation { get; set; }
    }
}
