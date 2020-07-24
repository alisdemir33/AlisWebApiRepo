using System;
using System.Collections.Generic;

namespace JWTSample.Models
{
    public partial class County
    {
        public County()
        {
            Foundation = new HashSet<Foundation>();
        }

        public int Id { get; set; }
        public string CountyName { get; set; }
        public int ProvinceId { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Foundation> Foundation { get; set; }
    }
}
