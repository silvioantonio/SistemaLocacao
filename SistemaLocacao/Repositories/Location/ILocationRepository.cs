using L = SistemaLocacao.Models;

namespace SistemaLocacao.Repositories.Location
{
    public interface ILocationRepository
    {
        public Task<IEnumerable<L.Location>> FindAll();
        public Task<L.Location?> FindById(int id);
        public Task<L.Location> Create(L.Location location);
        public Task<L.Location> Update(L.Location location);
        public Task<bool> Delete(int id);
        public Task<L.Location?> LocationByClientIdMovieId(int clientId, int movieId);
    }
}
