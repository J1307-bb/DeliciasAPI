using DeliciasAPI.Context;
using DeliciasAPI.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliciasAPI.Services
{
    public class TermsService : ITermsService
    {
        private readonly ApplicationDbContext _context;

        public TermsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Terms>>> GetTerms()
        {
            try
            {
                List<Terms> terms = await _context.Terms
                .Include(q => q.TermsItem)
                .ToListAsync();

                return new Response<List<Terms>>(terms);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }


        public async Task<Response<Terms>> GetTerm(int id)
        {
            try
            {
                Terms response = await _context.Terms.Include(q => q.TermsItem).FirstOrDefaultAsync(x => x.IdTerms == id);
                return new Response<Terms>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error: " + ex.Message);
            }
        }

        public async Task<Response<Terms>> CreateTerm(TermsResponse request)
        {
            try
            {
                // Crear
                Terms terms = new Terms()
                {
                    Title = request.Title,
                    TermsItem = request.TermsItem.Select(qi => new TermsItem
                    {
                        Term = qi.Term
                    }).ToList()
                };

                _context.Terms.Add(terms);
                await _context.SaveChangesAsync();

                return new Response<Terms>(terms);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        // Actualizar
        public async Task<Response<Terms>> UpdateTerms(int id, TermsResponse request)
        {
            try
            {
                Terms term = await _context.Terms
                     .Include(q => q.TermsItem)
                     .FirstOrDefaultAsync(x => x.IdTerms == id);

                if (term == null)
                {
                    return new Response<Terms>("Term not found.");
                }

                term.Title = request.Title;

                // Actualizar los QuoteItems
                term.TermsItem.Clear();
                foreach (var item in term.TermsItem)
                {
                    term.TermsItem.Add(new TermsItem
                    {
                        Term = item.Term
                    });
                }

                _context.Terms.Update(term);
                await _context.SaveChangesAsync();

                // Preparar la respuesta
                Terms response = new Terms
                {
                    Title = term.Title,
                    TermsItem = term.TermsItem.Select(qi => new TermsItem
                    {
                        Term = qi.Term
                    }).ToList()
                };

                return new Response<Terms>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar" + ex.Message);
            }
        }

        // Eliminar
        public async Task<Response<Terms>> DeleteTerm(int id)
        {
            try
            {
                Terms term = await _context.Terms
                    .Include (q => q.TermsItem)
                    .FirstOrDefaultAsync(x => x.IdTerms == id);
                if (term != null)
                {
                    _context.Terms.Remove(term);
                    await _context.SaveChangesAsync();
                    return new Response<Terms>("Usuario eliminado:" + term.IdTerms.ToString());
                }

                return new Response<Terms>("Quote no encontrado: " + id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar" + ex.Message);
            }

        }

    }
}
