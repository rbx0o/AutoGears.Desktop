using System;
using System.Threading.Tasks;
using Supabase;

namespace AutoGears.Services
{
    public sealed class SupabaseService
    {
        private static readonly Lazy<SupabaseService> _instance = new(() => new SupabaseService());
        public static SupabaseService Instance => _instance.Value;

        public Client? Supabase { get; private set; }

        private SupabaseService() { }

        public async Task InitAsync()
        {
            if (Supabase == null)
            {
                var url = "https://dursbdtlewjqxkphvqkf.supabase.co";
                var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImR1cnNiZHRsZXdqcXhrcGh2cWtmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDE2OTA0NDAsImV4cCI6MjA1NzI2NjQ0MH0.4oOTGnrJVzqNcVpCMhrehKA8HIXUz9tyB_hz7QULcb0";

                Supabase = new Client(url, key);
                await Supabase.InitializeAsync();
            }
        }
    }
}
