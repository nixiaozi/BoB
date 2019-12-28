using Autofac;
using BoB.ContainManager;
using BoB.UseBus.Register;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace BoB.UseBus.Startup
{
    public class BaseStartup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            
        }


        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            BaseRegister.RegisterConfigureContainer(builder);
            //BoBContainer.Container = builder;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Debug.WriteLine("Current EnvironmentName:" + env.EnvironmentName) ;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();




            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
