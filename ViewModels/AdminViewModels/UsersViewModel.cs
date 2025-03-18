using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using AutoGears.Models.Entities;
using AutoGears.Models.Queries;
using ReactiveUI;

namespace AutoGears.ViewModels.AdminViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<Person> _persons;
        private HashSet<Person> ModifiedPersons = new();
        #endregion

        #region Properties
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set => this.RaiseAndSetIfChanged(ref _persons, value);
        }
        #endregion

        #region Constructors
        public UsersViewModel()
        {
            _ = InitializeAsync();
            SaveChangesCommand = ReactiveCommand.CreateFromTask(SaveChanges);
        }

        private async Task InitializeAsync()
        {
            var temp = await Get.AllPersons();
            Persons = new ObservableCollection<Person>(temp);

            foreach (var person in Persons)
            {
                person.PropertyChanged += (s, e) => ModifiedPersons.Add(person);
            }
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
        #endregion

        #region Methods
        private async Task SaveChanges()
        {
            if (!ModifiedPersons.Any()) return;

            await Update.Persons(ModifiedPersons.ToList());

            ModifiedPersons.Clear();
        }
        #endregion
    }
}
