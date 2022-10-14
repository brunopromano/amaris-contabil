namespace AmarisContabil.Domain.Dtos
{
    public class LancamentoEditadoDto
    {
        public int Id { get; set; }
        public DateTime DataLancamento { get; set; }
        public char TipoLancamento { get; set; }
        public decimal ValorBrl { get; set; }
    }
}
