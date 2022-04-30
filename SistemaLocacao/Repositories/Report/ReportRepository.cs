using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Context;
using SistemaLocacao.Models.Reports;

namespace SistemaLocacao.Repositories.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly MySQLContext _context;

        public ReportRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportLocation>> FindAllClients(DateTime searchDate)
        {
            var response = _context.ReportsLocation.FromSqlRaw(
                "select l.id as LocationId, l.data_locacao as LeaseDate, l.data_devolucao as ReturnDate, c.id as ClientId, c.nome as Name, c.cpf as CPF, f.id as MovieId, f.lancamento as Lounch  from Locacao l " +
                "inner join Cliente c on c.id = l.id_cliente "+
                "inner join Filme f on f.id = l.id_filme "+
                "where l.data_locacao >= {0}", searchDate.Date);
            return await response.ToListAsync();
        }

        public async Task<IEnumerable<ReportMovie>> FindAllMovies()
        {
            var response = _context.ReportsMovie.FromSqlRaw( "select f.id as Id, f.titulo as Title from Filme f where f.id not in (select l.id_filme from Locacao l)" );
            return await response.ToListAsync();
        }

        public async Task<IEnumerable<ReportMovie>> FindTopMovies()
        {
            var response = _context.ReportsMovie.FromSqlRaw(
                "select f.id as Id, f.titulo as Title from Filme f "+
                "inner join Locacao l on l.id_filme = f.id "+
                "where YEAR(l.data_locacao) = (YEAR(CURDATE()) - 1) "+
                "GROUP by f.id, f.titulo ORDER by COUNT(*) DESC limit 5"
                );
            return await response.ToListAsync();
        }
        
        public async Task<IEnumerable<ReportMovie>> FindDownMovies()
        {
            var response = _context.ReportsMovie.FromSqlRaw(
                "select f.id as Id, f.titulo as Title from Filme f "+
                "inner join Locacao l on l.id_filme = f.id "+
                "where l.data_locacao BETWEEN DATE_SUB(NOW(), INTERVAL 7 DAY) and NOW() " +
                "GROUP by f.id, f.titulo ORDER by COUNT(*) ASC limit 3"
                );
            return await response.ToListAsync();
        }
        
        public async Task<IEnumerable<ReportClient>> FindSecondBestClient()
        {
            var response = _context.ReportsClient.FromSqlRaw(
                "SELECT DISTINCT c.id as Id, c.nome  as Name  FROM Cliente c "+
                "inner join Locacao l on l.id_cliente = c.id "+
                "GROUP by c.id, c.nome  ORDER by COUNT(*) DESC LIMIT 1, 1"
                );
            return await response.ToListAsync();
        }
    }
}
