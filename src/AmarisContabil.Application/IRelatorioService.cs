﻿using AmarisContabil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmarisContabil.Application
{
    public interface IRelatorioService
    {
        Task<List<SaldoDiario>> GerarSaldoConsolidadoPorDia();
    }
}