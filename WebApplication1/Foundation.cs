//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Foundation
    {
        public long ID { get; set; }
        public string FName { get; set; }
        public int CountyID { get; set; }
        public int ProvinceID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
    
        public virtual County County { get; set; }
        public virtual Province Province { get; set; }
    }
}