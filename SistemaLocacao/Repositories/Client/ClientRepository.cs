using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Context;

namespace SistemaLocacao.Repositories.Client
{
    public class ClientRepository : IClientRepository
    {
        private readonly MySQLContext _context;

        public ClientRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Models.Client> Create(Models.Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> Delete(int id)
        {
            var client = await FindById(id);
            if(client == null)
            {
                return false;
            }

            _context.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Models.Client>> FindAll()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Models.Client?> FindById(int id)
        {
            var client = await _context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();
            return client;
        }

        public async Task<Models.Client> Update(Models.Client client)
        {
            _context.Update(client);
            await _context.SaveChangesAsync(true);
            return client;
        }
    }
}
