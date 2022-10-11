namespace AmarisContabil.Domain;

public class Lancamento
{
    public int Id { get; set; }

    public DateTime DataHoraInsercao { get; set; }
    
    public DateOnly DataLancamento { get; set; }
    
    public char TipoLancamento { get; set; }
    
    public decimal ValorBrl { get; set; }
}
