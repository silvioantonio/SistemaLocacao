using L = SistemaLocacao.Models;

namespace SistemaLocacao.Services.Location
{
    public interface ILocationService
    {
        public Task<IEnumerable<L.Location>> FindAll();
        public Task<L.Location?> FindById(int id);
        public Task<L.Location> Create(L.Location location);
        public Task<L.Location> Update(L.Location location);
        public Task<bool> Delete(int id);
    }
}
