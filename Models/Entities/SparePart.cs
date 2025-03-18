using System;
using System.ComponentModel;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{
    [Table("spare_parts")]
    public class SparePart : BaseModel, INotifyPropertyChanged
    {
        private double _purchasePrice, _salePrice;
        private int _remains;

        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("article_number")]
        public string ArticleNumber { get; set; } = string.Empty;

        [Column("purchase_price")]
        public double PurchasePrice { get => _purchasePrice; set { _purchasePrice = value; OnPropertyChanged(nameof(PurchasePrice)); } }

        [Column("sale_price")]
        public double SalePrice { get => _salePrice; set { _salePrice = value; OnPropertyChanged(nameof(SalePrice)); } }

        [Column("remains")]
        public int Remains { get => _remains; set { _remains = value; OnPropertyChanged(nameof(Remains)); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
