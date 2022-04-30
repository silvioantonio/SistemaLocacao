using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Context;

namespace SistemaLocacao.Repositories.Location
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MySQLContext _context;

        public LocationRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Models.Location> Create(Models.Location location)
        {
            _context.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> Delete(int id)
        {
            var location = await FindById(id);
            if(location == null)
            {
                return false;
            }

            _context.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Models.Location>> FindAll()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Models.Location?> FindById(int id)
        {
            var location = await _context.Locations.Where(c => c.Id == id).FirstOrDefaultAsync();
            return location;
        }

        public async Task<Models.Location> Update(Models.Location location)
        {
            _context.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Models.Location?> LocationByClientIdMovieId(int clientId, int movieId)
        {
            var location = await _context.Locations.AsNoTracking().Where(c => c.ClientId == clientId && c.MovieId == movieId).FirstOrDefaultAsync();
            return location;
        }
    }
}
