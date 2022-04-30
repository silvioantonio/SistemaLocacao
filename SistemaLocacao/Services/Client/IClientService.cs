using C = SistemaLocacao.Models;

namespace SistemaLocacao.Services.Client
{
    public interface IClientService
    {
        public Task<IEnumerable<C.Client>> FindAll();
        public Task<C.Client?> FindById(int id);
        public Task<C.Client> Create(C.Client client);
        public Task<C.Client> Update(C.Client client);
        public Task<bool> Delete(int id);
    }
}
