using System.ComponentModel.DataAnnotations.Schema;

namespace AmarisContabil.Domain;

public class Lancamento
{
    public int Id { get; set; }

    public DateTime DataHoraInsercao { get; set; }
    
    public DateTime DataLancamento { get; set; }
    
    public char TipoLancamento { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    public decimal ValorBrl { get; set; }
}
