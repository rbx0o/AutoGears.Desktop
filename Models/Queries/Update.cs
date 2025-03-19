using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoGears.Models.Entities;
using AutoGears.Services;

namespace AutoGears.Models.Queries
{
    internal static class Update
    {
        public static async Task SpareParts(List<SparePart> parts)
        {
			try
			{
                foreach (var part in parts)
                {
                    await SupabaseService.Instance.Supabase
                        .From<SparePart>()
                        .Where(x => x.Id == part.Id)
                        .Set(x => x.PurchasePrice, part.PurchasePrice)
                        .Set(x => x.SalePrice, part.SalePrice)
                        .Set(x => x.Remains, part.Remains)
                        .Update();
                }
                Debug.WriteLine("Update SpareParts Success");
            }
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
                throw;
			}
        }

        public static async Task Suppliers(List<Supplier> suppliers)
        {
            try
            {
                foreach (var supplier in suppliers)
                {
                    await SupabaseService.Instance.Supabase
                        .From<Supplier>()
                        .Where(x => x.Id == supplier.Id)
                        .Set(x => x.PhoneNumber, supplier.PhoneNumber)
                        .Set(x => x.Email, supplier.Email)
                        .Set(x => x.Address, supplier.Address)
                        .Update();
                }
                Debug.WriteLine("Update Suppliers Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task Persons(List<Person> persons)
        {
            try
            {
                foreach (var person in persons)
                {
                    await SupabaseService.Instance.Supabase
                        .From<Person>()
                        .Where(x => x.Id == person.Id)
                        .Set(x => x.LastName, person.LastName)
                        .Set(x => x.FirstName, person.FirstName)
                        .Set(x => x.Patronymic, person.Patronymic)
                        .Set(x => x.PhoneNumber, person.PhoneNumber)
                        .Update();
                }
                Debug.WriteLine("Update Persons Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task Purchases(List<Purchase> purchases)
        {
            try
            {
                foreach (var purchase in purchases)
                {
                    purchase.PurchaseDate = purchase.SelectedDate.Date + purchase.SelectedTime;

                    await SupabaseService.Instance.Supabase
                        .From<Purchase>()
                        .Where(x => x.Id == purchase.Id)
                        .Set(x => x.PurchaseDate, purchase.PurchaseDate)
                        .Update();
                }
                Debug.WriteLine("Update Purchases Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task Orders(List<Order> orders)
        {
            try
            {
                foreach (var order in orders)
                {
                    await SupabaseService.Instance.Supabase
                        .From<Order>()
                        .Where(x => x.Id == order.Id)
                        .Set(x => x.OrderStatusId, order.SelectedStatus.Id)
                        .Update();
                }
                Debug.WriteLine("Update Orders Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
