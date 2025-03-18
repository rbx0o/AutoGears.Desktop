using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using AutoGears.Models.Entities;
using AutoGears.Models.Queries;
using ReactiveUI;

namespace AutoGears.ViewModels.AdminViewModels
{
    public class SuppliersViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Supplier> _suppliers;
        private HashSet<Supplier> ModifiedSuppliers = new();
        #endregion

        #region Properties
        public ObservableCollection<Supplier> Suppliers
        {
            get => _suppliers;
            set => this.RaiseAndSetIfChanged(ref _suppliers, value);
        }
        #endregion

        #region Constructors
        public SuppliersViewModel()
        {
            _ = InitializeAsync();
            SaveChangesCommand = ReactiveCommand.CreateFromTask(SaveChanges);
        }

        private async Task InitializeAsync()
        {
            var temp = await Get.AllSuppliers();
            Suppliers = new ObservableCollection<Supplier>(temp);

            foreach (var supplier in Suppliers)
            {
                supplier.PropertyChanged += (s, e) => ModifiedSuppliers.Add(supplier);
            }
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
        #endregion

        #region Methods
        private async Task SaveChanges()
        {
            if (!ModifiedSuppliers.Any()) return;

            await Update.Suppliers(ModifiedSuppliers.ToList());

            ModifiedSuppliers.Clear();
        }
        #endregion
    }
}
