using M = SistemaLocacao.Models;

namespace SistemaLocacao.Services.Movie
{
    public interface IMovieService
    {
        public Task<M.Movie?> FindById(int id);
        public Task Upload(IFormFile file);
    }
}
