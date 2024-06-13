using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface IQuotesService
    {
        public Task<Response<List<Quote>>> GetQuotes();
        public Task<Response<Quote>> GetQuote(int id);
        public Task<Response<Quote>> CreateQuote(QuoteResponse request);
        public Task<Response<Quote>> UpdateQuote(int id, QuoteResponse quote);
        public Task<Response<Quote>> DeleteQuote(int id);

    }
}
