using DeliciasAPI.Context;
using Domain.DTO;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;

namespace DeliciasAPI.Services
{
    public class PreferenceService
    {
        private readonly ApplicationDbContext _context;

        public PreferenceService(ApplicationDbContext context)
        {
            _context = context;
            //Mi Access Token de MercadoPago
            //MercadoPagoConfig.AccessToken = "TEST-6464142611890367-071916-50e2e212dda80f7f298586af9dc2f6d1-256625144";
            // Access Token de prueba
            MercadoPagoConfig.AccessToken = "APP_USR-6884658167676352-071923-a4d28406b596e0be008ec6153f0303f0-1907640687";
        }

        public class ItemModel
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public int UnitPrice { get; set; }
            public int Quantity { get; set; }
        }

        public async Task<Preference> CreatePreferenceAsync(List<PreferenceResponse> items, int idUser)
        {
            var preferenceRequest = new PreferenceRequest
            {
                Items = items.Select(item => new PreferenceItemRequest
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    PictureUrl = item.PictureUrl,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    CurrencyId = "MXN",
                }).ToList(),
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "http://localhost:5173/",
                    Failure = "http://localhost:5173/",
                    Pending = "http://localhost:5173/"
                },
                AutoReturn = "approved",
                NotificationUrl = "https://www.delicias.somee.com/MercadoPago/webhook-notification/" + idUser
            };

            var client = new PreferenceClient();
            var preference = await client.CreateAsync(preferenceRequest);
            return preference;
        }

        public async Task<Payment> GetPaymentDetailsAsync(long paymentId)
        {
            var client = new PaymentClient();
            Payment payment = await client.GetAsync(paymentId);
            Console.WriteLine("Payment received: " + JsonConvert.SerializeObject(payment));
            return payment;
        }

    }
}
