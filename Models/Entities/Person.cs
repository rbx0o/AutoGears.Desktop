using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("persons")]
    public class Person : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("patronymic")]
        public string Patronymic { get; set; } = string.Empty;

        [Column("birthday")]
        public DateOnly Birthday { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("gender_id")]
        [Reference(typeof(Gender))]
        public Guid GenderId { get; set; }

        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
