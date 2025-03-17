using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGears.Models.Entities;
using AutoGears.Services;

namespace AutoGears.Models.Queries
{
    internal static class Get
    {
        public static async Task<string?> CurrentUserRole()
        {
            try
            {
                var parameters = new Dictionary<string, object> { { "user_uuid", SupabaseService.Instance.Supabase.Auth.CurrentUser.Id } };

                var rpc = await SupabaseService.Instance.Supabase?.Rpc("get_role_id", parameters);
                string rbcID = rpc.Content.Trim(['\"']);

                var result = await SupabaseService.Instance.Supabase
                    .From<Role>()
                    .Where(x => x.Id == new Guid(rbcID))
                    .Single();

                Debug.WriteLine("CurrentUserRole Succes");
                return result?.Name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CurrentUserRole Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<Person?> CurrentUserPerson()
        {
            try
            {
                var parameters = new Dictionary<string, object> { { "user_uuid", SupabaseService.Instance.Supabase.Auth.CurrentUser.Id } };

                var rpc = await SupabaseService.Instance.Supabase?.Rpc("get_person_id", parameters);
                string rbcID = rpc.Content.Trim(['\"']);

                var result = await SupabaseService.Instance.Supabase
                    .From<Person>()
                    .Where(x => x.Id == new Guid(rbcID))
                    .Single();

                Debug.WriteLine("CurrentUserPerson Succes");
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"CurrentUserPerson Error: {ex.Message}");
                throw;
            }
        }
    }
}
