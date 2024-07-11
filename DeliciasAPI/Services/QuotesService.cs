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
                List<Quote> response = await _context.Quotes.Include(x => x.User).ToListAsync();
                return new Response<List<Quote>>(response);
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
                Quote quote = new Quote()
                {
                    Place = request.Place,
                    Date = request.Date,
                    IdUser = request.IdUser,
                    Status = request.Status,
                    QuoteItems = request.QuoteItems.Select(x => new QuoteItem()
                    {
                        IdMeal = x.IdMeal,
                        Quantity = x.Quantity
                    }).ToList()
                };

                _context.Quotes.Add(quote);
                await _context.SaveChangesAsync();

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
                Quote quo = await _context.Quotes.FirstOrDefaultAsync(x => x.IdQuote == id);

                if (quo != null)
                {
                    quo.Date = quote.Date;
                    quo.Place = quote.Place;
                    quo.Status = quote.Status;
                    quo.IdUser = quote.IdUser;
                    quo.QuoteItems = quote.QuoteItems.Select(x => new QuoteItem()
                    {
                        IdMeal = x.IdMeal,
                        Quantity = x.Quantity
                    }).ToList();
                    _context.SaveChanges();
                }

                Quote newQuote = new Quote()
                {
                    Place= quote.Place,
                    Date = quote.Date,
                    IdUser = quote.IdUser,
                    Status = quote.Status,
                    QuoteItems = quote.QuoteItems.Select(x => new QuoteItem()
                    {
                        IdMeal = x.IdMeal,
                        Quantity = x.Quantity
                    }).ToList()
                };

                _context.Quotes.Update(quo);
                await _context.SaveChangesAsync();

                return new Response<Quote>(newQuote);
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
