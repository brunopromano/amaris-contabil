using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;
using AmarisContabil.Infrastructure.Interfaces;
using AutoMapper;

namespace AmarisContabil.Application;

public class LancamentoService : ILancamentoService
{
    private readonly ILancamentoPersistencia _lancamentoPersistencia;
    private IMapper _mapper;

    public LancamentoService(ILancamentoPersistencia lancamentoPersistencia)
	{
		_lancamentoPersistencia = lancamentoPersistencia;

        var config = new MapperConfiguration(cfg => cfg.CreateMap<LancamentoDto, Lancamento>().ReverseMap());

        _mapper = config.CreateMapper();
    }

    public List<Lancamento> ObterTodosLancamentos()
    {
        return _lancamentoPersistencia.ObterTodosLancamentos();
    }

	public async Task<Lancamento> AdicionarLancamento(LancamentoDto lancamentoDto)
	{
		Lancamento lancamento = _mapper.Map<Lancamento>(lancamentoDto);

        _lancamentoPersistencia.Adicionar(lancamento);

        await _lancamentoPersistencia.SaveChangesAsync();

        return lancamento;
	}
}
