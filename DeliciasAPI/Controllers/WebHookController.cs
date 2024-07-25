using DeliciasAPI.Interfaces;
using DeliciasAPI.Services;
using Domain.DTO;
using Domain.Entities;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliciasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebHookController : ControllerBase
    {
        private readonly PreferenceService _mercadoPagoService;
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public WebHookController(PreferenceService mercadoPagoService, IOrderService orderService, IMailService mailService)
        {
            _mercadoPagoService = mercadoPagoService;
            _orderService = orderService;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookNotification notification)
        {
            try
            {
                // Imprime el objeto del webhook recibido en la consola
                Console.WriteLine("Webhook received: " + JsonConvert.SerializeObject(notification));


                return Ok();

            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }


    }


}
