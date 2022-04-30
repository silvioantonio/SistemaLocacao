namespace SistemaLocacao.Models.Reports
{
    public class ReportLocation
    {
        public int LocationId { get; set; }
        public int ClientId { get; set; }
        public int MovieId { get; set; }

        public DateTime LeaseDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public byte Lounch { get; set; }

        public string Name { get; set; }
        public string CPF { get; set; }
    }
}
