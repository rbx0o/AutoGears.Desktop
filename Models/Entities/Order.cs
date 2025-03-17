using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{

    [Table("orders")]
    public class Order : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("total_cost")]
        public double TotalCost { get; set; }

        [Column("order_status_id")]
        [Reference(typeof(OrderStatus))]
        public Guid OrderStatusId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("delivered_at")]
        public DateTime? DeliveredAt { get; set; } = null;

        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}
