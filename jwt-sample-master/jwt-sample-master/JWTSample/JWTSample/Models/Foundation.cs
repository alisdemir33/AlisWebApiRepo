using System;
using System.Collections.Generic;

namespace JWTSample.Models
{
    public partial class Foundation
    {
        public long Id { get; set; }
        public string Fname { get; set; }
        public int CountyId { get; set; }
        public int ProvinceId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public virtual County County { get; set; }
        public virtual Province Province { get; set; }
    }
}
