using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public static async Task<List<SparePart>> AllProducts()
        {
            try
            {
                var result = await SupabaseService.Instance.Supabase
                    .From<SparePart>()
                    .Get();

                Debug.WriteLine("AllProducts Succes");
                return result.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AllProducts Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<Supplier>> AllSuppliers()
        {
            try
            {
                var result = await SupabaseService.Instance.Supabase
                    .From<Supplier>()
                    .Get();

                Debug.WriteLine("AllSuppliers Succes");
                return result.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AllSuppliers Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<Person>> AllPersons()
        {
            try
            {
                var result = await SupabaseService.Instance.Supabase
                    .From<Person>()
                    .Get();

                Debug.WriteLine("AllPersons Succes");
                return result.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AllPersons Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<Purchase>> AllPurchases()
        {
            try
            {
                var result = await SupabaseService.Instance.Supabase
                    .From<Purchase>()
                    .Get();

                Debug.WriteLine("AllPurchases Succes");

                foreach (var item in result.Models)
                {
                    item.SelectedDate = new DateTimeOffset(item.PurchaseDate);
                    item.SelectedTime = item.PurchaseDate.TimeOfDay;
                }

                return result.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AllPurchases Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<PurchaseContentDto>> PurchaseContent(Guid purchaseId)
        {
            var temp1 = await SupabaseService.Instance.Supabase
                .From<PurchaseContent>()
                .Where(x => x.PurchaseId == purchaseId)
                .Get();

            var result = temp1.Models;

            var temp2 = await SupabaseService.Instance.Supabase
                .From<SparePart>()
                .Get();

            var parts = temp2.Models;

            return result.Select(x => new PurchaseContentDto
            {
                SparePart = parts.FirstOrDefault(p => p.Id == x.SparePartId),
                QuantityParts = x.QuantityParts
            }).ToList();
        }

        public static async Task<List<Order>> AllOrders()
        {
            try
            {
                var result = await SupabaseService.Instance.Supabase
                    .From<Order>()
                    .Get();

                Debug.WriteLine("AllOrders Success");
                return result.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AllOrders Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<OrderStatus>> AllOrderStatuses()
        {
            try
            {
                var result = await SupabaseService.Instance.Supabase
                    .From<OrderStatus>()
                    .Get();

                Debug.WriteLine("AllOrderStatuses Success");
                return result.Models;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AllOrderStatuses Error: {ex.Message}");
                throw;
            }
        }

        public static async Task<List<OrderContentDto>> OrderContent(Guid orderId)
        {
            var temp1 = await SupabaseService.Instance.Supabase
                .From<OrderContent>()
                .Where(x => x.OrderId == orderId)
                .Get();

            var result = temp1.Models;

            var temp2 = await SupabaseService.Instance.Supabase
                .From<SparePart>()
                .Get();

            var parts = temp2.Models;

            return result.Select(x => new OrderContentDto
            {
                SparePart = parts.FirstOrDefault(p => p.Id == x.SparePartId),
                QuantityParts = x.QuantityParts
            }).ToList();
        }
    }
}
