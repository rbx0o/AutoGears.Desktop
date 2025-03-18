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
    public class ProductViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<SparePart> _spareParts;
        private string _searchText = string.Empty;
        #endregion

        #region Properties
        public ObservableCollection<SparePart> SpareParts { get => _spareParts; set => this.RaiseAndSetIfChanged(ref _spareParts, value); }
        private HashSet<SparePart> ModifiedParts { get; set; } = new();
        public string SearchText { get => _searchText; set => this.RaiseAndSetIfChanged(ref _searchText, value); }
        #endregion

        #region Constructors
        public ProductViewModel()
        {
            _ = InitializeAsync();

            SaveChangesCommand = ReactiveCommand.CreateFromTask<SparePart>(SaveChanges);
        }

        private async Task InitializeAsync()
        {
            var temp = await Get.AllProducts();
            SpareParts = new ObservableCollection<SparePart>(temp);
            foreach (var part in SpareParts)
            {
                part.PropertyChanged += (s, e) => ModifiedParts.Add(part);
            }
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<SparePart, Unit> SaveChangesCommand { get; }
        #endregion

        #region Methods
        private async Task SaveChanges(SparePart part)
        {
            if (!ModifiedParts.Any()) return;

            Update.SpareParts(ModifiedParts.ToList());

            ModifiedParts.Clear();
        }
        #endregion

    }
}
