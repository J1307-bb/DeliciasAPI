using Domain.DTO;
using Domain.Entities;

namespace DeliciasAPI.Interfaces
{
    public interface ITermsService
    {
        public Task<Response<List<Terms>>> GetTerms();
        public Task<Response<Terms>> GetTerm(int id);
        public Task<Response<Terms>> CreateTerm(TermsResponse request);
        public Task<Response<Terms>> UpdateTerms(int id, TermsResponse quote);
        public Task<Response<Terms>> DeleteTerm(int id);

    }
}
