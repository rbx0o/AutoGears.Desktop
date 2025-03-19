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
    public class PurchasesViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Purchase> _purchases;
        private HashSet<Purchase> ModifiedPurchases = new();
        
        #endregion

        #region Properties
        public ObservableCollection<Purchase> Purchases { get => _purchases; set => this.RaiseAndSetIfChanged(ref _purchases, value); }        
        #endregion

        #region Constructors
        public PurchasesViewModel()
        {
            _ = InitializeAsync();
            SaveChangesCommand = ReactiveCommand.CreateFromTask(SaveChanges);
            GenerateInvoiceCommand = ReactiveCommand.CreateFromTask<Purchase>(GenerateInvoice);           
        }

        private async Task InitializeAsync()
        {
            var temp = await Get.AllPurchases();
            Purchases = new ObservableCollection<Purchase>(temp);

            foreach (var purchase in Purchases)
            {
                purchase.PropertyChanged += (s, e) => ModifiedPurchases.Add(purchase);
            }
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
        public ReactiveCommand<Purchase, Unit> GenerateInvoiceCommand { get; }
        #endregion

        #region Methods
        private async Task SaveChanges()
        {
            if (!ModifiedPurchases.Any()) return;

            await Update.Purchases(ModifiedPurchases.ToList());

            ModifiedPurchases.Clear();
        }

        private async Task GenerateInvoice(Purchase purchase)
        {
            if (purchase == null) return;

            var content = await Get.PurchaseContent(purchase.Id);
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Invoice_{purchase.PurchaseNumber}.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                await writer.WriteLineAsync($"Накладная № {purchase.PurchaseNumber}");
                await writer.WriteLineAsync($"Дата: {purchase.PurchaseDate:dd.MM.yyyy}");
                await writer.WriteLineAsync("\nСостав поставки:\n");

                foreach (var item in content)
                {
                    await writer.WriteLineAsync($"• {item.SparePart.Name} - {item.QuantityParts} шт.");
                }
            }

            Debug.WriteLine($"Накладная сохранена: {filePath}");
        }
        #endregion
    }
}
