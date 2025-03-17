using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("spare_parts_compatibility")]
    public class SparePartsCompatibility : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("spare_part_id")]
        [Reference(typeof(SparePart))]
        public Guid SparePartId { get; set; }

        [Column("model_id")]
        [Reference(typeof(CarBrandModel))]
        public Guid ModelId { get; set; }
    }
}
