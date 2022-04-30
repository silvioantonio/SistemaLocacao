using SistemaLocacao.Models.Reports;
using R = SistemaLocacao.Models;

namespace SistemaLocacao.Services.Report
{
    public interface IReportService
    {
        public Task<IEnumerable<R.Client>> ListLateCostumers(DateTime searchDate);
        public Task<IEnumerable<ReportMovie>> ListDownMoviesLocated();
        public Task<IEnumerable<ReportMovie>> ListMoviesNotLocated();
        public Task<IEnumerable<ReportMovie>> ListTopMoviesLocated();
        public Task<IEnumerable<ReportClient>> ListSecondBestClient();
        public Task DownloadReport(DateTime searchDate);
    }
}
