using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMailService _mailService;

        public QuotesService(ApplicationDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // Lista de Quotes
        public async Task<Response<List<Quote>>> GetQuotes()
        {
            try
            {
                List<Quote> quotes = await _context.Quotes
                .Include(q => q.User)
                .Include(q => q.QuoteItems)
                    .ThenInclude(qi => qi.Meal)
                .ToListAsync();

                return new Response<List<Quote>>(quotes);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Obtener un solo Quote
        public async Task<Response<Quote>> GetQuote(int id)
        {
            try
            {
                Quote response = await _context.Quotes.FirstOrDefaultAsync(x => x.IdQuote == id);
                return new Response<Quote>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        // Crear Quote
        public async Task<Response<Quote>> CreateQuote(QuoteResponse request)
        {
            try
            {
                // Crear la cotización
                Quote quote = new Quote()
                {
                    Place = request.Place,
                    NumMeals = request.NumMeals,
                    Date = request.Date,
                    IdUser = request.IdUser,
                    Status = request.Status,
                    TotalPrice = request.TotalPrice,
                    QuoteItems = request.QuoteItems.Select(qi => new QuoteItem
                    {
                        IdMeal = qi.IdMeal,
                        Quantity = qi.Quantity
                    }).ToList()
                };

                _context.Quotes.Add(quote);
                await _context.SaveChangesAsync();

                try
                {
                    //Buscar el usuario que solicito la cotización
                    User user = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == request.IdUser);
                    if (user != null) {

                        string htmlContent = @"
                            <!DOCTYPE html>
                            <html lang='en'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>Agradecimiento por Cotización</title>
                                <style>
                                    body {
                                        font-family: Arial, sans-serif;
                                        background-color: #f4f4f4;
                                        margin: 0;
                                        padding: 0;
                                    }
                                    .container {
                                        width: 100%;
                                        max-width: 600px;
                                        margin: 0 auto;
                                        background-color: #ffffff;
                                        padding: 20px;
                                        border-radius: 8px;
                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                    }
                                    .header {
                                        text-align: center;
                                        padding: 10px 0;
                                    }
                                    .header img {
                                        max-width: 100px;
                                    }
                                    .content {
                                        margin: 20px 0;
                                        line-height: 1.6;
                                    }
                                    .content h1 {
                                        color: #333333;
                                    }
                                    .content p {
                                        color: #555555;
                                    }
                                    .footer {
                                        text-align: center;
                                        margin: 20px 0;
                                        color: #777777;
                                        font-size: 12px;
                                    }
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <div class='header'>
                                        <img src='cid:logo' alt='Delicias Logo'>
                                    </div>
                                    <div class='content'>
                                        <h1>¡Gracias por tu Cotización!</h1>
                                        <p>Hola " + user.Name + ' ' + user.LastName + @",</p>
                                        <p>Gracias por realizar una cotización con Delicias. Hemos recibido tu solicitud y nuestro equipo está trabajando en ella.</p>
                                        <p>Muy pronto te enviaremos una respuesta con todos los detalles de tu cotización.</p>
                                        <p>Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en contactarnos.</p>
                                        <p>Gracias por confiar en nosotros.</p>
                                        <p>Saludos cordiales,</p>
                                        <p>El equipo de Delicias</p>
                                    </div>
                                    <div class='footer'>
                                        <p>&copy; 2024 Delicias. Todos los derechos reservados.</p>
                                    </div>
                                </div>
                            </body>
                            </html>";

                        string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/DeliciasLogo.svg");

                        //Enviar correo al usuario
                        _mailService.SendMail(user.Email, "Cotización 📈", htmlContent, logoPath);
                    }

                }
                catch(Exception ex)
                {
                    throw new Exception("Ocurrio un error al enviar el correo" + ex.Message);
                }

                // Preparar la respuesta
                QuoteResponse response = new QuoteResponse
                {
                    Place = quote.Place,
                    NumMeals = quote.NumMeals,
                    Date = quote.Date,
                    IdUser = quote.IdUser,
                    TotalPrice = quote.TotalPrice,
                    QuoteItems = quote.QuoteItems.Select(qi => new QuoteItemResponse
                    {
                        IdMeal = qi.IdMeal,
                        Quantity = qi.Quantity
                    }).ToList()
                };

                return new Response<Quote>(quote);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar Quote
        public async Task<Response<Quote>> UpdateQuote(int id, QuoteResponse quote)
        {
            try
            {
                Quote quo = await _context.Quotes
                     .Include(q => q.QuoteItems)
                     .FirstOrDefaultAsync(x => x.IdQuote == id);

                if (quo == null)
                {
                    return new Response<Quote>("Quote not found.");
                }

                quo.Date = quote.Date;
                quo.Place = quote.Place;
                quo.NumMeals = quote.NumMeals;
                quo.IdUser = quote.IdUser;
                quo.TotalPrice = quote.TotalPrice;
                quo.Status = quote.Status;

                // Actualizar los QuoteItems
                quo.QuoteItems.Clear();
                foreach (var item in quote.QuoteItems)
                {
                    quo.QuoteItems.Add(new QuoteItem
                    {
                        IdMeal = item.IdMeal,
                        Quantity = item.Quantity
                    });
                }

                _context.Quotes.Update(quo);
                await _context.SaveChangesAsync();

                // Preparar la respuesta
                Quote response = new Quote
                {
                    Place = quo.Place,
                    NumMeals = quo.NumMeals,
                    Date = quo.Date,
                    IdUser = quo.IdUser,
                    TotalPrice = quo.TotalPrice,
                    QuoteItems = quo.QuoteItems.Select(qi => new QuoteItem
                    {
                        IdMeal = qi.IdMeal,
                        Quantity = qi.Quantity
                    }).ToList()
                };

                return new Response<Quote>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar Quote
        public async Task<Response<Quote>> DeleteQuote(int id)
        {
            try
            {
                Quote quote = await _context.Quotes.FirstOrDefaultAsync(x => x.IdQuote == id);
                if (quote != null)
                {
                    _context.Quotes.Remove(quote);
                    await _context.SaveChangesAsync();
                    return new Response<Quote>("Usuario eliminado:" + quote.IdQuote.ToString());
                }

                return new Response<Quote>("Quote no encontrado: " + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
