using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Context;

namespace SistemaLocacao.Repositories.Movie
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MySQLContext _context;

        public MovieRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Models.Movie?> FindById(int id)
        {
            return await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task Upload(IEnumerable<Models.FileImport> records)
        {
            foreach (var record in records)
            {
                if (string.IsNullOrEmpty(record.Id))
                {
                    throw new ArgumentException("Todos os ids devem estar preenchidos");
                }

                var movie = _context.Movies.AsNoTracking().Where(l => l.Title.Equals(record.Titulo) && l.Lounch == byte.Parse(record.Lancamento)).FirstOrDefault();

                var newMovie = false;
                if (movie == null)
                {
                    movie = new Models.Movie();
                    newMovie = true;
                }

                movie.Id = int.Parse(record.Id);
                movie.Title = record.Titulo;
                movie.Lounch = byte.Parse(record.Lancamento);
                movie.ParentalRating = int.Parse(record.ClassificacaoIndicativa);

                if (newMovie)
                {
                    _context.Movies.Add(movie);
                }
                else
                {
                    _context.Movies.Update(movie);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
