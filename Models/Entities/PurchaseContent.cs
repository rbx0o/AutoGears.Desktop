using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("purchase_content")]
    public class PurchaseContent : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("purchase_id")]
        [Reference(typeof(Purchase))]
        public Guid PurchaseId { get; set; }

        [Column("spare_part_id")]
        [Reference(typeof(SparePart))]
        public Guid SparePartId { get; set; }

        [Column("quantity_parts")]
        public int QuantityParts { get; set; }
    }
}
