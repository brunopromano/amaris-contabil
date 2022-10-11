namespace AmarisContabil.Domain.Dtos
{
    public class LancamentoDto
    {
        public DateTime DataHoraInsercao => DateTime.Now;
        public DateTime DataLancamento { get; set; }
        public char TipoLancamento { get; set; }
        public decimal ValorBrl { get; set; }
    }
}
