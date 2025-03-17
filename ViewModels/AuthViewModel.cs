using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using AutoGears.Models.Queries;
using AutoGears.Services;
using AutoGears.ViewModels.SessionAuthorization;
using ReactiveUI;
using Supabase.Gotrue;
using static AutoGears.ViewModels.MainWindowViewModel;

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
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> SignInCommand { get; }
        #endregion

        #region Methods
        public async Task SignIn()
        {
            try
            {
                await SupabaseService.Instance.Supabase.Auth.SignIn(Email, Password);

                if (SupabaseService.Instance.Supabase?.Auth.CurrentUser != null)
                {
                    string role = await Get.CurrentUserRole();
                    Session? session;

                    switch (role)
                    {
                        case "Клиент":
                            Message = "Доступ запрещен ⛔";
                            break;
                        case "Администратор":
                            session = SupabaseService.Instance.Supabase.Auth.CurrentSession;
                            AuthorizedUserSession.UserInstance.SaveData(session.AccessToken, session.RefreshToken);
                            MainWindow.NavigateToAdminMain();
                            break;
                        case "Менеджер":
                            session = SupabaseService.Instance.Supabase.Auth.CurrentSession;
                            AuthorizedUserSession.UserInstance.SaveData(session.AccessToken, session.RefreshToken);
                            MainWindow.NavigateToManagerMain();
                            break;
                        default:
                            break;
                    }

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
