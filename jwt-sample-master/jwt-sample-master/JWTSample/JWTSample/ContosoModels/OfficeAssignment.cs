using System;
using System.Collections.Generic;

namespace JWTSample.ContosoModels
{
    public partial class OfficeAssignment
    {
        public Guid InstructorId { get; set; }
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
