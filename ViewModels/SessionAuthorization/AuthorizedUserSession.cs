using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGears.Services;
using ReactiveUI;

namespace AutoGears.ViewModels.SessionAuthorization
{
    public class AuthorizedUserSession : ViewModelBase
    {
        private static Lazy<AuthorizedUserSession> _userInstance = new(() => new AuthorizedUserSession());
        public static AuthorizedUserSession UserInstance = _userInstance.Value;

        public bool UserLoadedState { get ; private set; }

        public void SaveData(string email, string password)
        {
            SaveUserSession.SaveData(email, password);
        }

        public async Task SessionIsSaved()
        {
            AuthTokens? data = SaveUserSession.GetData();
            try
            {
                if (data != null && !string.IsNullOrEmpty(data.AccessToken) && !string.IsNullOrEmpty(data.RefreshToken))
                {
                    await SupabaseService.Instance.Supabase?.Auth.SetSession(data.AccessToken, data.RefreshToken); 
                    Debug.WriteLine($"SessionIsSaved Succes: {data.AccessToken}");
                    Debug.WriteLine($"SessionIsSaved Succes: {data.RefreshToken}");
                    Debug.WriteLine($"SessionIsSaved Succes: {SupabaseService.Instance.Supabase?.Auth.CurrentSession.AccessToken}");
                    Debug.WriteLine($"SessionIsSaved Succes: {SupabaseService.Instance.Supabase?.Auth.CurrentSession.RefreshToken}");
                    Debug.WriteLine($"SessionIsSaved Succes: {SupabaseService.Instance.Supabase?.Auth.CurrentUser.Email}");
                    UserLoadedState = true;
                    return;
                }
                UserLoadedState = false;
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SessionIsSaved Error: {ex.Message}");
                throw;
            }
        }

        public void DeleteData()
        {
            SaveUserSession.DeleteData();
        }
    }
}
