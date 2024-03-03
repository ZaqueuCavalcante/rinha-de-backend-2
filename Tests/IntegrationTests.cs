using API.Dtos;
using System.Net;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class IntegrationTests : IntegrationTestBase
{
    [Test]
    public async Task Deve_criar_o_banco_com_os_clientes_cadastrados_corretamente()
    {
        // Arrange
        using var ctx = _factory.GetDbCtx();

        // Act
        var clientes = await ctx.Clientes.ToListAsync();

        // Assert
        clientes.Should().HaveCount(5);
        clientes.ForEach(c => c.Saldo.Should().Be(0));
        clientes.First(c => c.Id == 1).Limite.Should().Be(1000_00);
        clientes.First(c => c.Id == 2).Limite.Should().Be(800_00);
        clientes.First(c => c.Id == 3).Limite.Should().Be(10_000_00);
        clientes.First(c => c.Id == 4).Limite.Should().Be(100_000_00);
        clientes.First(c => c.Id == 5).Limite.Should().Be(5000_00);
    }

    [Test]
    public async Task Deve_retornar_404_caso_o_cliente_informado_na_criacao_de_transacao_nao_exista()
    {
        // Arrange
        var client = _factory.CreateClient();

        var body = new TransacaoIn { };

        // Act
        var response = await client.PostAsJsonAsync("/clientes/6/transacoes", body);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task Deve_retornar_422_caso_uma_transacao_de_debito_tente_deixar_o_saldo_do_cliente_menor_que_seu_limite()
    {
        // Arrange
        var client = _factory.CreateClient();

        var body = new TransacaoIn { Tipo = 'd', Valor = 1000_01, Descricao = "Beleleibe" };

        // Act
        var response = await client.PostAsJsonAsync("/clientes/1/transacoes", body);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.UnprocessableContent);
    }

    [Test]
    public async Task Deve_retornar_200_caso_uma_transacao_de_debito_tente_deixar_o_saldo_do_cliente_igual_ao_seu_limite()
    {
        // Arrange
        var client = _factory.CreateClient();

        var body = new TransacaoIn { Tipo = 'd', Valor = 1000_00, Descricao = "Beleleibe" };

        // Act
        var response = await client.PostAsJsonAsync("/clientes/1/transacoes", body);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Test]
    public async Task Deve_retornar_404_caso_o_cliente_informado_na_busca_de_extrato_nao_exista()
    {
        // Arrange
        var client = _factory.CreateClient();

        var body = new TransacaoIn { };

        // Act
        var response = await client.GetAsync("/clientes/6/extrato");

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }
}
