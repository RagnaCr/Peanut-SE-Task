using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Peanut_SE_Task.Controllers;
using Peanut_SE_Task.Factories;
using Peanut_SE_Task.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Peanut_SE_Task
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient<BinanceService>(client =>
            {
                client.BaseAddress = new Uri("https://api.binance.com/");
            });
            services.AddHttpClient<KucoinService>(client =>
            {
                client.BaseAddress = new Uri("https://api.kucoin.com/");
            });

            services.AddSingleton<ExchangeServiceFactory>();

            services.AddScoped<IExchangeService>(provider =>
            {
                var factory = provider.GetRequiredService<ExchangeServiceFactory>();
                return factory.CreateBinanceService();
            });

            services.AddScoped<IExchangeService>(provider =>
            {
                var factory = provider.GetRequiredService<ExchangeServiceFactory>();
                return factory.CreateKucoinService();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
