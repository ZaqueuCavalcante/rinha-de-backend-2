using System;
using System.Collections.Generic;

namespace API.Dtos;

public class ExtratoOut
{
    /// <summary>
    /// Infos relacionadas ao cliente.
    /// </summary>
    public ExtratoSaldoOut Saldo { get; set; }

    /// <summary>
    /// Lista ordenada por data/hora das transações de forma decrescente contendo até as 10 últimas transações.
    /// </summary>
    public List<ExtratoTransacaoOut> UltimasTransacoes { get; set; }
}

public class ExtratoSaldoOut
{
    /// <summary>
    /// Saldo total atual do cliente (não apenas das últimas transações seguintes exibidas).
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// Data/hora da consulta do extrato.
    /// </summary>
    public DateTime DataExtrato { get; set; }

    /// <summary>
    /// Limite cadastrado do cliente.
    /// </summary>
    public int Limite { get; set; }
}

public class ExtratoTransacaoOut
{
    /// <summary>
    /// Valor da transação.
    /// </summary>
    public int Valor { get; set; }

    /// <summary>
    /// c -> crédito | d -> débito
    /// </summary>
    public char Tipo { get; set; }

    /// <summary>
    /// Descrição informada durante a transação.
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Data/hora da realização da transação.
    /// </summary>
    public DateTime RealizadaEm { get; set; }
}
