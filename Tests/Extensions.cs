using API.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public static class Extensions
{
    public static DbCtx GetDbCtx(this WebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<DbCtx>();
    }
}
