using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace AutoGears.ViewModels.SessionAuthorization
{
    public class SaveUserSession
    {
        public static string GetDataFilePath()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appFolder = Path.Combine(appDataPath, "AutoGears");
                if (!Directory.Exists(appFolder)) Directory.CreateDirectory(appFolder);
                return Path.Combine(appFolder, "session.json");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetDataFilePath Error: {ex.Message}");
                throw;
            }            
        }

        public static void SaveData(string email, string password)
        {
            try
            {
                AuthTokens data = new AuthTokens();
                data.AccessToken = email;
                data.RefreshToken = password;
                string path = GetDataFilePath();
                string json = JsonConvert.SerializeObject(data);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SaveData Error: {ex.Message}");
                throw;
            }
        }

        public static void DeleteData()
        {
            try
            {
                string path = GetDataFilePath();
                if (File.Exists(path)) File.Delete(path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DeleteData Error: {ex.Message}");
                throw;
            }            
        }

        public static AuthTokens? GetData()
        {
            try
            {
                string path = GetDataFilePath();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<AuthTokens>(json);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetData Error: {ex.Message}");
                throw;
            }
        }
    }
}
