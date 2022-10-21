using System;
using System.Collections.Generic;

namespace ApiCleanAr.Models
{
    public partial class Product
    {
        public int IdProducts { get; set; }
        public string? NameProducts { get; set; }
        public string? DescriptionProduct { get; set; }
        public int? Sctok { get; set; }
        public int? IdCategory { get; set; }
        public decimal? Price { get; set; }

        public virtual Category? oCategory { get; set; }
    }
}
