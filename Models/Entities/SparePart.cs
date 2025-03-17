using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("spare_parts")]
    public class SparePart : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("article_number")]
        public string ArticleNumber { get; set; } = string.Empty;

        [Column("purchase_price")]
        public double PurchasePrice { get; set; }

        [Column("sale_price")]
        public double SalePrice { get; set; }

        [Column("remains")]
        public int Remains { get; set; }
    }
}
