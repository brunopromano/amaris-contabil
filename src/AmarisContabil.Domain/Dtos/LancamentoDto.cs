namespace AmarisContabil.Domain.Dtos
{
    public class LancamentoDto
    {
        public DateOnly DataHoraInsercao { get; set; }
        public char TipoLancamento { get; set; }
        public decimal ValorBrl { get; set; }
    }
}
