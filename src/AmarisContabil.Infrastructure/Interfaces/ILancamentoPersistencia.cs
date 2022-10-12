using AmarisContabil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmarisContabil.Infrastructure.Interfaces
{
    public interface ILancamentoPersistencia : IPersistenciaGenerica
    {
        List<Lancamento> ObterTodosLancamentos();
    }
}
