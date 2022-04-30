using M = SistemaLocacao.Models;

namespace SistemaLocacao.Repositories.Movie
{
    public interface IMovieRepository
    {
        public Task<M.Movie?> FindById(int id);
        public Task Upload(IEnumerable<M.FileImport> records);
    }
}
