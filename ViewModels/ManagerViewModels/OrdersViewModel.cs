using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using AutoGears.Models.Entities;
using AutoGears.Models.Queries;
using ReactiveUI;

namespace AutoGears.ViewModels.ManagerViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Order> _orders;
        private ObservableCollection<OrderStatus> _orderStatuses;
        private HashSet<Order> ModifiedOrders = new();
        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get => _orders; set => this.RaiseAndSetIfChanged(ref _orders, value); }
        public ObservableCollection<OrderStatus> OrderStatuses { get => _orderStatuses; set => this.RaiseAndSetIfChanged(ref _orderStatuses, value); }
        #endregion

        #region Constructors
        public OrdersViewModel()
        {
            _ = InitializeAsync();
            SaveChangesCommand = ReactiveCommand.CreateFromTask(SaveChanges);
            GenerateReceiptCommand = ReactiveCommand.CreateFromTask<Order>(GenerateReceipt);
        }

        private async Task InitializeAsync()
        {
            var ordersTemp = await Get.AllOrders();
            Orders = new ObservableCollection<Order>(ordersTemp);

            var statusesTemp = await Get.AllOrderStatuses();
            OrderStatuses = new ObservableCollection<OrderStatus>(statusesTemp);

            foreach (var order in Orders)
            {
                order.SelectedStatus = OrderStatuses.FirstOrDefault(s => s.Id == order.OrderStatusId);
                order.PropertyChanged += (s, e) => ModifiedOrders.Add(order);
            }
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
        public ReactiveCommand<Order, Unit> GenerateReceiptCommand { get; }
        #endregion

        #region Methods
        private async Task SaveChanges()
        {
            if (!ModifiedOrders.Any()) return;

            await Update.Orders(ModifiedOrders.ToList());
            ModifiedOrders.Clear();
        }

        private async Task GenerateReceipt(Order order)
        {
            if (order == null) return;

            var content = await Get.OrderContent(order.Id);
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Receipt_{order.Id}.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                await writer.WriteLineAsync($"Заказ № {order.Id}");
                await writer.WriteLineAsync($"Дата создания: {order.CreatedAt:dd.MM.yyyy HH:mm}");
                await writer.WriteLineAsync($"Статус: {order.SelectedStatus.Name}");
                await writer.WriteLineAsync($"Сумма: {order.TotalCost:C2}");
                await writer.WriteLineAsync("\nСостав заказа:\n");

                foreach (var item in content)
                {
                    await writer.WriteLineAsync($"• {item.SparePart.Name} - {item.QuantityParts} шт.");
                }
            }

            Debug.WriteLine($"Чек сохранён: {filePath}");
        }
        #endregion
    }
}
