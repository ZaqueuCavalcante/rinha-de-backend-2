using System;
using Npgsql;
using API.Dtos;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, RinhaJsonSerializerContext.Default);
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddNpgsqlDataSource(
    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "DB_CONNECTION_STRING ERROR LALALA"
);

var limites = new Dictionary<int, int>
{
    {1,   1000 * 100},
    {2,    800 * 100},
    {3,  10000 * 100},
    {4, 100000 * 100},
    {5,   5000 * 100}
};

var functions = new Dictionary<string, string>
{
    { "c", "creditar" },
    { "d", "debitar" }
};

var app = builder.Build();

app.MapPost("/clientes/{id}/transacoes", async (int id, TransacaoIn data, NpgsqlConnection cnn) =>
{
    if (!limites.ContainsKey(id))
        return Results.NotFound();

    if (!data.EhValido())
        return Results.UnprocessableEntity();

    await using (cnn)
    {
        await cnn.OpenAsync();
        await using var cmd = cnn.CreateCommand();

        cmd.CommandText = $"SELECT novo_saldo, deu_erro, mensagem FROM {functions[data.Tipo]}($1, $2, $3)";
        cmd.Parameters.AddWithValue(id);
        cmd.Parameters.AddWithValue(data.Valor);
        cmd.Parameters.AddWithValue(data.Descricao);

        using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();

        if (reader.GetBoolean(1))
            return Results.UnprocessableEntity();

        return Results.Ok(new TransacaoOut { Saldo = reader.GetInt32(0), Limite = limites[id] });
    }
});

app.MapGet("/clientes/{id}/extrato", async (int id, NpgsqlConnection cnn) =>
{
    if (!limites.ContainsKey(id))
        return Results.NotFound();

    await using (cnn)
    {
        await cnn.OpenAsync();

        await using var cmd = cnn.CreateCommand();
        cmd.CommandText = @"
            (
                SELECT saldo, 'saldo' AS tipo, 'saldo' AS descricao, NOW() AS realizada_em
                FROM clientes
                WHERE id = $1
            )
            UNION ALL
            (
                SELECT valor, tipo, descricao, realizada_em
                FROM transacoes
                WHERE cliente_id = $1
                ORDER BY id DESC
                LIMIT 10
            )
        ";
        cmd.Parameters.AddWithValue(id);

        using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();

        var response = new ExtratoOut
        {
            Saldo = new ExtratoSaldoOut
            {
                Total = reader.GetInt32(0),
                DataExtrato = reader.GetDateTime(3),
                Limite = limites[id]
            },
            UltimasTransacoes = [],
        };

        while (await reader.ReadAsync())
        {
            response.UltimasTransacoes.Add(
                new ExtratoTransacaoOut
                {
                    Valor = reader.GetInt32(0),
                    Tipo = reader.GetString(1),
                    Descricao = reader.GetString(2),
                    RealizadaEm = reader.GetDateTime(3),
                }
            );
        }

        return Results.Ok(response);
    }
});

app.Run();

public partial class Program { }
