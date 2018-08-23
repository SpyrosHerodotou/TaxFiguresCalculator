using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using TaxFiguresCalculator;
using TaxFiguresCalculator.Infrastracture.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FunctionalTests
{
    public class CustomApplicationWebFactory<TStartup>
:WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection().AddEntityFrameworkSqlServer()
                        .BuildServiceProvider();

                    // Add a database context (ApplicationDbContext) using an in-memory 
                    // database for testing.
                    services.AddDbContext<Tax_Figures_CalculatorContext>(options =>
                    {
                        options.UseSqlServer("Server=WCYSH185195-U9B; Database=Tax_Figures_Calculator;User Id = sa; Password = admin");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    // context (ApplicationDbContext).
                    using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<Tax_Figures_CalculatorContext>();
                    var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();
                    
                        // Ensure the database is created.
                        db.Database.EnsureCreated();

                }
            });
        }
    }
}