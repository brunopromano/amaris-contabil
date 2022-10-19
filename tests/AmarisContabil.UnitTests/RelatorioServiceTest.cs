using AmarisContabil.Application;
using AmarisContabil.Domain;
using AmarisContabil.Infrastructure.Interfaces;
using Moq;
using Xunit.Sdk;

namespace AmarisContabil.UnitTests;

public class RelatorioServiceTest
{
    #region Variáveis
    private Mock<ILancamentoPersistencia> _mockLancamentoPersistencia;
    private IRelatorioService _relatorioService;
    #endregion


    #region Inicialização
    public RelatorioServiceTest()
    {
        _mockLancamentoPersistencia = new Mock<ILancamentoPersistencia>();
        
        _relatorioService = new RelatorioService(_mockLancamentoPersistencia.Object);
    }
    #endregion


    #region Testes
    [Fact]
    public void GerarSaldoConsolidadoPorDia_DeveRetornarLista()
    {
        List<SaldoDiario> listSaldoDiario = new List<SaldoDiario>()
        {
            new SaldoDiario
            {
                DiaConsolidado = DateTime.Now,
                TotalCredito = 100.0M,
                TotalDebito = 50.0M,
                SaldoDia = 50.0M 
            },
            new SaldoDiario
            {
                DiaConsolidado = DateTime.Now,
                TotalCredito = 1500.0M,
                TotalDebito = 100.0M,
                SaldoDia = 1400.0M
            }
        };

        var retorno = _mockLancamentoPersistencia.Setup(x => x.GerarSaldoDiario()).Returns(Task.FromResult(listSaldoDiario));

        var response = _relatorioService.GerarSaldoConsolidadoPorDia();

        Assert.NotNull(response);
    }

    [Fact]
    public void GerarSaldoConsolidadoPorDia_DeveRetornarNulo()
    {
        List<SaldoDiario>? listSaldoDiario = null;

        var retorno = _mockLancamentoPersistencia.Setup(x => x.GerarSaldoDiario()).Returns(Task.FromResult(listSaldoDiario));

        var response = _relatorioService.GerarSaldoConsolidadoPorDia();

        Assert.Null(response.Result);

    }

    [Fact]
    public void GerarSaldoConsolidadoPorDia_DeveLancarException()
    {
        var retorno = _mockLancamentoPersistencia.Setup(x => x.GerarSaldoDiario()).Throws(new Exception());

        Assert.ThrowsAsync<Exception>(() => _relatorioService.GerarSaldoConsolidadoPorDia());
    }
    #endregion
}