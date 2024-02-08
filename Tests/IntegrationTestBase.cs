using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests;

public class IntegrationTestBase
{
    protected WebAppFactory _factory = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new WebAppFactory();
    }

    [SetUp]
    public async Task SetUp()
    {
        using var ctx = _factory.GetDbCtx();
        await ctx.Database.EnsureDeletedAsync();
        await ctx.Database.EnsureCreatedAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.DisposeAsync();
    }
}
