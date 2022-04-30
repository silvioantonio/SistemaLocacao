using SistemaLocacao.Models.Reports;

namespace SistemaLocacao.Repositories.Report
{
    public interface IReportRepository
    {
        public Task<IEnumerable<ReportLocation>> FindAllClients(DateTime searchDate);
        public Task<IEnumerable<ReportClient>> FindSecondBestClient();
        public Task<IEnumerable<ReportMovie>> FindAllMovies();
        public Task<IEnumerable<ReportMovie>> FindDownMovies();
        public Task<IEnumerable<ReportMovie>> FindTopMovies();
    }
}
