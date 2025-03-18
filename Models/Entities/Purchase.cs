using System;
using System.ComponentModel;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("purchases")]
    public class Purchase : BaseModel, INotifyPropertyChanged
    {
        private DateTime _purchaseDate;

        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("purchase_number")]
        public int PurchaseNumber { get; set; }

        [Column("purchase_date")]
        public DateTime PurchaseDate
        {
            get => _purchaseDate;
            set { _purchaseDate = value; OnPropertyChanged(nameof(PurchaseDate)); }
        }

        [Column("supplier_id")]
        [Reference(typeof(Supplier))]
        public Guid SupplierId { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
