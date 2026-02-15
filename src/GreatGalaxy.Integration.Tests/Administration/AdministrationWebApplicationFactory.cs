using LiteDB;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GreatGalaxy.Integration.Tests.Administration
{
    // Explicitly reference the correct Program type to resolve CS0433
    using AdministrationProgram = GreatGalaxy.Administration.Program;

    public class AdministrationWebApplicationFactory : WebApplicationFactory<AdministrationProgram>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing LiteDB registration
                services.RemoveAll<LiteDatabase>();

                // Replace with in-memory LiteDB
                services.AddSingleton(new LiteDatabase(":memory:"));
            });
        }
    }
}
