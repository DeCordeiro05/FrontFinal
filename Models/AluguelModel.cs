namespace Api.Models
{
    public class AluguelModel
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public int anoNascimento { get; set; }
        public int livroId { get; set; }
        public DateTime emprestado_em { get; set; }
        public DateTime? devolvido_em { get; set; }
    }
}
