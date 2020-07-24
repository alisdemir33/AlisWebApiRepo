using System;
using System.Collections.Generic;

namespace JWTSample.Models
{
    public partial class AçıkKapıSosyalHizmetBaş1
    {
        public string Id { get; set; }
        public string İl { get; set; }
        public string İlçe { get; set; }
        public string TcKimlikNo { get; set; }
        public string CepTelNo { get; set; }
        public string Adı { get; set; }
        public string Soyadı { get; set; }
        public DateTime? BasvuruTarihi { get; set; }
        public string BasvuruKonu { get; set; }
    }
}
