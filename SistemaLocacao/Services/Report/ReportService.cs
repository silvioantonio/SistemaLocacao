using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SistemaLocacao.Repositories.Report;
using System.Data;

namespace SistemaLocacao.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportrepository)
        {
            _reportRepository = reportrepository;
        }

        public async Task<IEnumerable<Models.Client>> ListLateCostumers(DateTime searchDate)
        {
            var reportClients = await _reportRepository.FindAllClients(searchDate);

            var leaseDate = searchDate;
            List<Models.Client> lateCostumers = new();

            var reportClientsArr = reportClients.ToArray();

            for (int i = 0; i < reportClientsArr.Length; i++)
            {
                int days;
                if (reportClientsArr[i].Lounch == 1)
                {
                    days = 2;
                }
                else
                {
                    days = 3;
                }

                leaseDate.Date.AddDays(days);
                if (leaseDate.Date.CompareTo(DateTime.Now.Date) < 0 && reportClientsArr[i].ReturnDate.Date.CompareTo(DateTime.MinValue.Date) == 0)
                {
                    if (!lateCostumers.Exists(c => c.Id == reportClientsArr[i].ClientId))
                    {
                        lateCostumers.Add(new Models.Client
                        {
                            Id = reportClientsArr[i].ClientId,
                            CPF = reportClientsArr[i].CPF,
                            Name = reportClientsArr[i].Name
                        });
                    }                    
                }
            }

            return lateCostumers;
        }

        public async Task<IEnumerable<Models.Reports.ReportMovie>> ListMoviesNotLocated()
        {
            var response = await _reportRepository.FindAllMovies();
            return response.ToList();
        }

        public async Task<IEnumerable<Models.Reports.ReportMovie>> ListTopMoviesLocated()
        {
            var response = await _reportRepository.FindTopMovies();
            return response.ToList();
        }
        
        public async Task<IEnumerable<Models.Reports.ReportMovie>> ListDownMoviesLocated()
        {
            var response = await _reportRepository.FindDownMovies();
            return response.ToList();
        }
        
        public async Task<IEnumerable<Models.Reports.ReportClient>> ListSecondBestClient()
        {
            var response = await _reportRepository.FindSecondBestClient();
            return response.ToList();
        }

        public async Task DownloadReport(DateTime searchDate)
        {
            List<Models.Client> listLateCostumers = (List<Models.Client>)await ListLateCostumers(searchDate);
            List<Models.Reports.ReportMovie> listMoviesNotLocated = (List<Models.Reports.ReportMovie>)await ListMoviesNotLocated();
            List<Models.Reports.ReportMovie> listTopMoviesLocated = (List<Models.Reports.ReportMovie>)await ListTopMoviesLocated();
            List<Models.Reports.ReportMovie> listDownMoviesLocated = (List<Models.Reports.ReportMovie>)await ListDownMoviesLocated();
            List<Models.Reports.ReportClient> listSecondBestClient = (List<Models.Reports.ReportClient>)await ListSecondBestClient();

            DataTable listLateCostumersTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listLateCostumers), typeof(DataTable));
            DataTable listSecondBestClientTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listSecondBestClient), typeof(DataTable));
            DataTable listMoviesNotLocatedTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listMoviesNotLocated), typeof(DataTable));
            DataTable listTopMoviesLocatedTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listTopMoviesLocated), typeof(DataTable));
            DataTable listDownMoviesLocatedTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listDownMoviesLocated), typeof(DataTable));

            var listDataTables = new List<DataTable> { listLateCostumersTable, listSecondBestClientTable, listMoviesNotLocatedTable, listTopMoviesLocatedTable, listDownMoviesLocatedTable };

            var tempDirPath = $@"{Environment.GetEnvironmentVariable("USERPROFILE")}\Downloads";
            using (var fs = new FileStream($"{tempDirPath}/RelatoriosSistemaLocacao.xlsx", FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook = new XSSFWorkbook();

                for (int i = 0; i < listDataTables.Count; i++)
                {
                    ISheet excelSheet = workbook.CreateSheet("Sheet " + i);

                    List<String> columns = new List<string>();

                    IRow row = excelSheet.CreateRow(0);
                    int columnIndex = 0;

                    foreach (System.Data.DataColumn column in listDataTables[i].Columns)
                    {
                        columns.Add(column.ColumnName);
                        row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                        columnIndex++;
                    }

                    int rowIndex = 1;
                    foreach (DataRow dsrow in listDataTables[i].Rows)
                    {
                        row = excelSheet.CreateRow(rowIndex);
                        int cellIndex = 0;
                        foreach (String col in columns)
                        {
                            row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                            cellIndex++;
                        }

                        rowIndex++;
                    }
                }

                workbook.Write(fs);
            }
        }
    }
}