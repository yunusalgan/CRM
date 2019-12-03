using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CRM.Bootstrapper.Modules;
using CRM.WebApi.Middlewares;
using Microsoft.Extensions.Logging;

namespace CRM.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApiVersioning();
            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("CrmCors",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            services.AddAutofac();

            var cbuilder = new ContainerBuilder();
            cbuilder.Populate(services);

            cbuilder.RegisterModule<ServiceModule>();
            cbuilder.RegisterModule<RepositoryModule>();

            ApplicationContainer = cbuilder.Build();


            // DB configlerini app 'e atmak için kullanılır
            //var result = ApplicationContainer.Resolve<IProjectParametersService>().GetConfigs();

            //if (result.StatusCode == 1)
            //{
            //    ApplicationOptions.asd = result.Data.asd;
            //}




            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();

            app.UseCors("CrmCors");

            // If you want to dispose of resources that have been resolved in the 
            // application container, register for the "ApplicationStopped" event. 
            // You can only do this if you have a direct reference to the container, 
            // so it won't work with the above ConfigureContainer mechanism. 
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());

            loggerFactory.AddEventLog();

        }
    }
}
