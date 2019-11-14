using mass_transit.MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiContrib.Core;

namespace mass_transit
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
            services.AddHostedService<>()
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseBranchWithServices("/api", 
                services => { 
                    services.AddControllers();
                    services.AddEventBus();
                }, api =>
            {
                
                if (env.IsDevelopment())
                {
                    api.UseDeveloperExceptionPage();
                }

                api.UseHttpsRedirection();

                api.UseRouting();

                api.UseAuthorization();

                api.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            });
            app.UseBranchWithServices("/api2", 
            services => { services.AddControllers(); }, api =>
            {
                if (env.IsDevelopment())
                {
                    api.UseDeveloperExceptionPage();
                }

                api.UseHttpsRedirection();

                api.UseRouting();

                api.UseAuthorization();

                api.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            });
        }
    }
}