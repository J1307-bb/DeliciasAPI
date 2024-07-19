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

        public QuotesService(ApplicationDbContext context)
        {
            _context = context;
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
