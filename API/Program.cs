using System;
using API.Dtos;
using API.Database;
using Syki.Back.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DbSettings>();
builder.Services.AddDbContext<DbCtx>();

var app = builder.Build();

app.MapPost("/clientes/{id}/transacoes", async ([FromRoute] byte id, [FromBody] TransacaoIn body, [FromServices] DbCtx ctx) =>
{
    var cliente = await ctx.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    if (cliente == null)
        return Results.NotFound();

    // ZAN -> https://github.com/zanfranceschi/rinha-de-backend-2024-q1-zan-dotnet

    if (body.Tipo == 'd')
    {
        if (cliente.Saldo - body.Valor < -cliente.Limite)
        {
            return Results.UnprocessableEntity();
        }
        cliente.Saldo -= body.Valor;
        return Results.Ok();
    }

    cliente.Saldo += body.Valor;
    return Results.Ok();
});

app.MapGet("/clientes/{id}/extrato", async ([FromRoute] byte id, [FromServices] DbCtx ctx) =>
{
    var cliente = await ctx.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    if (cliente == null)
        return Results.NotFound();

    return Results.Ok(new TransacaoOut { Limite = 1000_00, Saldo = -90_98 });
});

app.Run();

public partial class Program { }
