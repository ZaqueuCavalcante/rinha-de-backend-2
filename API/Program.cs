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
    var clienteExiste = await ctx.Clientes.AnyAsync(c => c.Id == id);
    if (!clienteExiste)
        return Results.NotFound();

    return Results.Ok(new TransacaoOut { Limite = 1000_00, Saldo = -90_98 });
});

app.MapGet("/clientes/{id}/extrato", ([FromRoute] byte id) =>
{
    return Results.Ok(new TransacaoOut { Limite = 1000_00, Saldo = -90_98 });
});

app.Run();

public partial class Program { }
