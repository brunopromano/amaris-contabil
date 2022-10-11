using AmarisContabil.Domain.Dtos;
using AmarisContabil.Infrastructure.Interfaces;

namespace AmarisContabil.Application;

public class LancamentoService : ILancamentoService
{
    private readonly ILancamentoPersistencia _lancamentoPersistencia;

	public LancamentoService(ILancamentoPersistencia lancamentoPersistencia)
	{
		_lancamentoPersistencia = lancamentoPersistencia;
	}

	public void AdicionarLancamento(LancamentoDto lancamentoDto)
	{
		// Fazer mapeamento com automapper
	}
}
