using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("car_brands")]
    public class CarBrand : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
