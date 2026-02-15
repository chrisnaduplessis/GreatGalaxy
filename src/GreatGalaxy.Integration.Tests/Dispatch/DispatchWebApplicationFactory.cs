using LiteDB;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GreatGalaxy.Integration.Tests.Dispatch
{
    // Explicitly reference the correct Program type to resolve CS0433
    using AdministrationProgram = GreatGalaxy.Dispatch.Program;

    public class DispatchWebApplicationFactory : WebApplicationFactory<AdministrationProgram>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing LiteDB registration
                services.RemoveAll<LiteDatabase>();

                // Replace with in-memory LiteDB
                services.AddSingleton(new LiteDatabase(":memory:"));

                // Replace MassTransit transport with in-memory
                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(AdministrationProgram).Assembly);

                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
        }
    }
}
