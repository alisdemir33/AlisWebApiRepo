using System;
using System.Collections.Generic;

namespace JWTSample.Models
{
    public partial class Province
    {
        public Province()
        {
            County = new HashSet<County>();
            Foundation = new HashSet<Foundation>();
        }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public virtual ICollection<County> County { get; set; }
        public virtual ICollection<Foundation> Foundation { get; set; }
    }
}
