using CsvHelper.Configuration.Attributes;

namespace SistemaLocacao.Models
{
    public class FileImport
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Titulo { get; set; }

        [Index(2)]
        public string ClassificacaoIndicativa { get; set; }

        [Index(3)]
        public string Lancamento { get; set; }
    }
}
