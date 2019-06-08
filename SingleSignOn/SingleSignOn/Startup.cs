using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjection = SingleSignOn.StartupServices.DependencyInjection;
using GlobalSettings = SingleSignOn.StartupServices.GlobalSettings;
using CachingStartup = SingleSignOn.StartupServices.Caching;
using Swashbuckle.AspNetCore.Swagger;

namespace SingleSignOn
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            new GlobalSettings.Startup().Start(Configuration);
            new DependencyInjection.Startup().Start(services);
            new CachingStartup.Startup().Start(services);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Single Sign On - API",
                        Version = "v1",
                        Description = "Single Sign On Microservice",
                        Contact = new Contact
                        {
                            Name = "Otávio Larrosa",
                            Url = "https://github.com/otaviolarrosa/SingleSignOn"
                        }
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                ///swagger/v1/
                c.SwaggerEndpoint("swagger.json", "Single Sign On Api");
                c.RoutePrefix = "swagger";
            });


            app.UseMvc();
        }
    }
}
