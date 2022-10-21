using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiCleanAr.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int IdCategory { get; set; }
        public string? DescriptionCategory { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
