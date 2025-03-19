using System;
using System.ComponentModel;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AutoGears.Models.Entities
{

    [Table("orders")]
    public class Order : BaseModel, INotifyPropertyChanged
    {
        private OrderStatus _selectedStatus;

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

        public OrderStatus SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; OnPropertyChanged(nameof(SelectedStatus)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
