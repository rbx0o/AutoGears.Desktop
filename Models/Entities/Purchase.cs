using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("purchases")]
    public class Purchase : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("purchase_number")]
        public int PurchaseNumber { get; set; }

        [Column("purchase_date")]
        public DateTime PurchaseDate { get; set; }

        [Column("supplier_id")]
        [Reference(typeof(Supplier))]
        public Guid SupplierId { get; set; }
    }
}
