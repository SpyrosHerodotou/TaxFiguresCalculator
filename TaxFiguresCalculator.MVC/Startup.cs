using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaxFiguresCalculator.Core.Interfaces;
using TaxFiguresCalculator.Core.Repositories;
using TaxFiguresCalculator.Core.Services;
using TaxFiguresCalculator.Infrastracture.DataAccess;
using TaxFiguresCalculator.Infrastructure;
using TaxFiguresCalculator.MVC.Filters;
using TaxFiguresCalculator.MVC.Interfaces;
using TaxFiguresCalculator.MVC.Services;

namespace TaxFiguresCalculator
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<Tax_Figures_CalculatorContext>(c =>
            {
                try
                {
                    c.UseSqlServer(Configuration.GetConnectionString("TaxFiguresConnection"));
                }
                catch (System.Exception ex)
                {
                    var message = ex.Message;
                }
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
            });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IFileUploadService, FileUploaderService>();
            services.AddScoped<IDataManagerViewService, DataManagerViewService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DisableFormValueModelBindingAttribute));
            });
            // Add memory cache services
            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            _services = services;

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Customer}/{action=Index}/{id?}");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Edit",
                    template: "{controller=DataManager}/{action=Edit}/{transactionId?}");
            });
        }
    }
}
