using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using AutoGears.Services;
using AutoGears.ViewModels.SessionAuthorization;
using ReactiveUI;
using Supabase.Gotrue;
using static AutoGears.ViewModels.MainWindowViewModel;

namespace AutoGears.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MainViewModel()
        {
            ToggleThemeCommand = ReactiveCommand.Create(Navigate.ToggleTheme);
            ExitCommand = ReactiveCommand.CreateFromTask(Exit);
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> ToggleThemeCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        #endregion

        #region Methods
        private async Task Exit()
        {
            try
            {
                await SupabaseService.Instance.Supabase?.Auth.SignOut();
                AuthorizedUserSession.UserInstance.DeleteData();
                Navigate.ToAuth();
                Debug.WriteLine($"Exit Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exit Error: {ex.Message}");
            }
        }
        #endregion
    }
}
