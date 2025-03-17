using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("order_content")]
    public class OrderContent : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("order_id")]
        [Reference(typeof(Order))]
        public Guid OrderId { get; set; }

        [Column("spare_part_id")]
        [Reference(typeof(SparePart))]
        public Guid SparePartId { get; set; }

        [Column("quantity_parts")]
        public int QuantityParts { get; set; }
    }
}
