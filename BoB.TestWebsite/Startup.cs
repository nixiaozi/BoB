using Autofac;
using BoB.UseBus.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BoB.TestWebsite
{
    public class Startup:BaseStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
        }


        public override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
