using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GreatGalaxy.Integration.Tests.Reporting
{
    // Explicitly reference the correct Program type to resolve CS0433
    using ReportingProgram = GreatGalaxy.Reporting.Program;

    public class ReportingWebApplicationFactory : WebApplicationFactory<GreatGalaxy.Reporting.Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Replace MassTransit transport with in-memory
                services.AddMassTransitTestHarness(x =>
                {
                    x.AddConsumers(typeof(ReportingProgram).Assembly);

                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
        }
    }
}
