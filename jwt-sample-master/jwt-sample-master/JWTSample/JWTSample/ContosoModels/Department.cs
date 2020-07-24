using System;
using System.Collections.Generic;

namespace JWTSample.ContosoModels
{
    public partial class Department
    {
        public Department()
        {
            Course = new HashSet<Course>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public Guid? InstructorId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
