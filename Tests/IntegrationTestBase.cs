using Dapper;
using Npgsql;
using System;
using System.IO;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests;

public class IntegrationTestBase
{
    protected WebAppFactory _factory = null!;
    protected string cnnString;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebAppFactory();
        _factory.CreateClient();
        cnnString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    [SetUp]
    public async Task SetUp()
    {
        FileInfo file = new(@"C:\Users\Zaqueu\rinha-de-backend-2\init.sql");
        string script = file.OpenText().ReadToEnd();

        using var connection = new NpgsqlConnection(cnnString);
        await connection.ExecuteScalarAsync(script);
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.DisposeAsync();
    }
}
