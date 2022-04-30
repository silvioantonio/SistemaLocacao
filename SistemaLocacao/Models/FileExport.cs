namespace SistemaLocacao.Models
{
    public class FileExport
    {
        public string nome { get; set; }
        public string tipo { get; set; }
        public byte[] byteArray { get; set; }
        public string text { get; set; }

        public FileExport(string _nome, string _tipo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            nome = _nome;
            tipo = _tipo;
        }
    }
}
