using AmarisContabil.Application;
using AmarisContabil.Domain;
using AmarisContabil.Domain.Dtos;
using AmarisContabil.Infrastructure;
using AmarisContabil.Infrastructure.Interfaces;
using Moq;

namespace AmarisContabil.UnitTests
{
    public class LancamentoServiceTeste
    {
        #region Variáveis
        private readonly Mock<ILancamentoPersistencia> _mockLancamentoPersistencia;
        private readonly ILancamentoService _lancamentoService;
        #endregion

        #region Inicialização
        public LancamentoServiceTeste()
        {
            _mockLancamentoPersistencia = new Mock<ILancamentoPersistencia>();

            _lancamentoService = new LancamentoService(_mockLancamentoPersistencia.Object);
        }
        #endregion

        #region Testes
        [Fact]
        public void ObterLancamentoPorId_DeveRetornarLancamento()
        {
            Lancamento lancamento = new Lancamento()
            {
                Id = 1,
                DataHoraInsercao = DateTime.Now,
                DataLancamento = DateTime.Now,
                TipoLancamento = 'D',
                ValorBrl = 100.00M
            };

            var lancamentoObtido = _mockLancamentoPersistencia.Setup(x => x.ObterLancamentoPorId(It.IsAny<int>())).Returns(lancamento);

            var response = _lancamentoService.ObterLancamentoPorId(It.IsAny<int>());

            Assert.NotNull(response);
        }

        [Fact]
        public void ObterLancamentoPorId_DeveRetornarNulo()
        {
            Lancamento? lancamento = null;

            var lancamentoObtido = _mockLancamentoPersistencia.Setup(x => x.ObterLancamentoPorId(It.IsAny<int>())).Returns(lancamento);

            var response = _lancamentoService.ObterLancamentoPorId(It.IsAny<int>());

            Assert.Null(response);
        }

        [Fact]
        public void ObterTodosLancamentos_DeveRetornarLista()
        {
            List<Lancamento> listaLancamentos = new List<Lancamento>()
            {
                new Lancamento
                {
                    Id = 1,
                    DataHoraInsercao = DateTime.Now,
                    DataLancamento = DateTime.Now,
                    TipoLancamento = 'D',
                    ValorBrl = 100.00M
                },
                new Lancamento
                {
                    Id = 2,
                    DataHoraInsercao = DateTime.Now,
                    DataLancamento = DateTime.Now,
                    TipoLancamento = 'C',
                    ValorBrl = 200.00M
                }
            };

            _mockLancamentoPersistencia.Setup(x => x.ObterTodosLancamentos()).Returns(listaLancamentos);

            var retorno = _lancamentoService.ObterTodosLancamentos();

            Assert.NotNull(retorno);
            Assert.IsType<List<Lancamento>>(retorno);
        }

        [Fact]
        public void ObterTodosLancamentos_DeveRetornarNulo()
        {
            List<Lancamento>? listaLancamentos = null;

            _mockLancamentoPersistencia.Setup(x => x.ObterTodosLancamentos()).Returns(listaLancamentos);

            var retorno = _lancamentoService.ObterTodosLancamentos();

            Assert.Null(retorno);
        }

        [Fact]
        public void AdicionarLancamento_DeveRetornarLancamento()
        {
            LancamentoDto lancamentoDto = new LancamentoDto()
            {
                DataLancamento = DateTime.Now,
                TipoLancamento = 'D',
                ValorBrl = 100.00M
            };

            _mockLancamentoPersistencia.Setup(x => x.Adicionar(It.IsAny<Lancamento>()));

            var retorno = _lancamentoService.AdicionarLancamento(lancamentoDto).Result;

            Assert.NotNull(retorno);
            Assert.IsType<Lancamento>(retorno); 
        }

        [Fact]
        public void AdicionarLancamento_DeveRetornarNulo()
        {
            LancamentoDto? lancamentoDto = null;

            _mockLancamentoPersistencia.Setup(x => x.Adicionar(It.IsAny<Lancamento>()));

            var retorno = _lancamentoService.AdicionarLancamento(lancamentoDto).Result;

            Assert.Null(retorno);
        }

        [Fact]
        public void AtualizarLancamento_DeveRetornarLancamento()
        {
            Lancamento lancamento = new Lancamento()
            {
                Id = 1,
                DataHoraInsercao = DateTime.Now,
                DataLancamento = DateTime.Now,
                TipoLancamento = 'D',
                ValorBrl = 100.00M
            };

            _mockLancamentoPersistencia.Setup(x => x.Atualizar(It.IsAny<Lancamento>()));
            _mockLancamentoPersistencia.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(true));

            var retorno = _lancamentoService.AtualizarLancamento(lancamento).Result;

            Assert.NotNull(retorno);
            Assert.IsType<Lancamento>(retorno);
        }

        [Fact]
        public void AtualizarLancamento_DeveRetornarNulo()
        {
            Lancamento? lancamento = null;

            _mockLancamentoPersistencia.Setup(x => x.Atualizar(It.IsAny<Lancamento>()));

            var retorno = _lancamentoService.AtualizarLancamento(lancamento).Result;

            Assert.Null(retorno);
        }

        [Fact]
        public void ExcluirLancamento_DeveRetornarTrue()
        {
            _mockLancamentoPersistencia.Setup(x => x.ObterLancamentoPorId(It.IsAny<int>())).Returns(new Lancamento());
            _mockLancamentoPersistencia.Setup(x => x.Remover(It.IsAny<Lancamento>()));
            _mockLancamentoPersistencia.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(true));

            var response = _lancamentoService.ExcluirLancamento(It.IsAny<int>());

            Assert.True(response.Result);
        }

        [Fact]
        public void ExcluirLancamento_DeveRetornarFalse()
        {
            _mockLancamentoPersistencia.Setup(x => x.ObterLancamentoPorId(It.IsAny<int>())).Returns(new Lancamento());
            _mockLancamentoPersistencia.Setup(x => x.Remover(It.IsAny<Lancamento>()));
            _mockLancamentoPersistencia.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(false));

            var response = _lancamentoService.ExcluirLancamento(It.IsAny<int>());

            Assert.False(response.Result);
        }
        #endregion
    }
}
