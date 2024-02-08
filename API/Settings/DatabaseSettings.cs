using Microsoft.Extensions.Configuration;

namespace Syki.Back.Settings;

public class DbSettings
{
    public string ConnectionString { get; set; }

    public DbSettings(IConfiguration configuration)
    {
        configuration.GetSection("Database").Bind(this);
    }
}
