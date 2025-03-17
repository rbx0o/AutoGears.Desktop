using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("suppliers")]
    public class Supplier : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("inn")]
        public string Inn { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;
    }
}
