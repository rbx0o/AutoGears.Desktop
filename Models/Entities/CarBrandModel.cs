using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("car_brand_models")]
    public class CarBrandModel : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("car_brand_id")]
        [Reference(typeof(CarBrand))]
        public Guid CarBrandId { get; set; }
    }
}
