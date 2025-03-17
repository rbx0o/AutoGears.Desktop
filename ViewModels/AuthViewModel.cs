using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using AutoGears.Services;
using AutoGears.ViewModels.SessionAuthorization;
using ReactiveUI;
using static AutoGears.ViewModels.MainWindowViewModel;
using static AutoGears.ViewModels.ViewModelBase;

namespace AutoGears.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        #region Fields
        private string _email = string.Empty, _password = string.Empty, _message = string.Empty;
        #endregion

        #region Properties
        public string Email { get => _email; set => this.RaiseAndSetIfChanged(ref _email, value); }
        public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
        public string Message { get => _message; set => this.RaiseAndSetIfChanged(ref _message, value); }
        #endregion

        #region Constructors
        public AuthViewModel()
        {
            SignInCommand = ReactiveCommand.CreateFromTask(SignIn);
            ToggleThemeCommand = ReactiveCommand.Create(Navigate.ToggleTheme);
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> SignInCommand { get; }

        public ReactiveCommand<Unit, Unit> ToggleThemeCommand { get; }
        #endregion

        #region Methods
        public async Task SignIn()
        {
            try
            {
                await SupabaseService.Instance.Supabase.Auth.SignIn(Email, Password);

                if (SupabaseService.Instance.Supabase?.Auth.CurrentUser != null)
                {
                    var session = SupabaseService.Instance.Supabase.Auth.CurrentSession;
                    AuthorizedUserSession.UserInstance.SaveData(session.AccessToken, session.RefreshToken);
                    Navigate.ToMain();
                    Debug.WriteLine($"SignIn Success");
                }
            }
            catch (Exception ex)
            {
                Message = "Что-то пошло не так 🥲";
                Debug.WriteLine($"SignIn Error: {ex.Message}");
            }
        }
        #endregion
    }
}
