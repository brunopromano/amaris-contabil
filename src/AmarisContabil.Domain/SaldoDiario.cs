namespace AmarisContabil.Domain
{
    public class SaldoDiario
    {
        public DateTime DiaConsolidado { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal SaldoDia { get; set; }
    }
}
