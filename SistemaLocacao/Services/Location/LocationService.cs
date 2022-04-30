using SistemaLocacao.Repositories.Location;
using SistemaLocacao.Services.Client;
using SistemaLocacao.Services.Movie;

namespace SistemaLocacao.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IClientService _clientService;
        private readonly IMovieService _movieService;
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository, IClientService clientService, IMovieService movieService)
        {
            _locationRepository = locationRepository;
            _clientService = clientService;
            _movieService = movieService;
        }

        public async Task<Models.Location> Create(Models.Location location)
        {
            var client = await _clientService.FindById(location.ClientId);
            if(client == null)
            {
                throw new ArgumentException("Id do cliente invalido!");
            }

            var movie = await _movieService.FindById(location.MovieId);
            if (movie == null)
            {
                throw new ArgumentException("Id do filme invalido!");
            }

            var hasLocationClientMovie = await LocationByClientIdMovieId(location.ClientId, location.MovieId);
            if (hasLocationClientMovie != null)
            {
                throw new ArgumentException("já existe uma locação desse filme para esse usuario!");
            }

            location.LeaseDate = DateTime.Now;
            location.ReturnDate = DateTime.MinValue;

            return await _locationRepository.Create(location);
        }

        public async Task<bool> Delete(int id)
        {
            return await _locationRepository.Delete(id);
        }

        public async Task<IEnumerable<Models.Location>> FindAll()
        {
            return await _locationRepository.FindAll();
        }

        public async Task<Models.Location?> FindById(int id)
        {
            return await _locationRepository.FindById(id);
        }

        public async Task<Models.Location> Update(Models.Location location)
        {
            var client = await _clientService.FindById(location.ClientId);
            if (client == null)
            {
                throw new ArgumentException("Id do cliente invalido!");
            }

            var movie = await _movieService.FindById(location.MovieId);
            if (movie == null)
            {
                throw new ArgumentException("Id do filme invalido!");
            }

            var locationClientMovie = await LocationByClientIdMovieId(location.ClientId, location.MovieId);
            if (locationClientMovie != null && locationClientMovie.Id != location.Id && locationClientMovie.ReturnDate.CompareTo(DateTime.MinValue) > 0 )
            {
                throw new ArgumentException("já existe uma locação ativa desse filme para esse usuario!");
            }

            return await _locationRepository.Update(location);
        }

        private async Task<Models.Location?> LocationByClientIdMovieId(int clientId, int movieId)
        {
            var location = await _locationRepository.LocationByClientIdMovieId(clientId, movieId);
            return location;
        }

    }
}
