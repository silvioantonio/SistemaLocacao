using CsvHelper;
using CsvHelper.Configuration;
using SistemaLocacao.Repositories.Movie;
using System.Globalization;
using M = SistemaLocacao.Models;

namespace SistemaLocacao.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<M.Movie?> FindById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id do filme invalido!");
            }

            return await _movieRepository.FindById(id);
        }

        public async Task Upload(IFormFile file)
        {            
            var fileextension = Path.GetExtension(file.FileName);

            if (fileextension == ".csv")
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                };
                Stream stream = file.OpenReadStream();

                using var reader = new StreamReader(stream);

                using var csv = new CsvReader(reader, config);
                var records = csv.GetRecords<M.FileImport>();
                await _movieRepository.Upload(records);
            }
            else
            {
                throw new Exception("Apenas arquivos com extensao CSV podem ser enviados!");
            }
        }
    }
}
