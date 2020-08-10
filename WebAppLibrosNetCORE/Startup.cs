using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAppLibrosNetCORE.Context;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Helpers;
using WebAppLibrosNetCORE.ModelsDTO;
using WebAppLibrosNetCORE.Services;

namespace WebAppLibrosNetCORE
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
            //IHOSTEDSERVICE para ejecutar tareas en segundo plano
            // services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, WriteToFileHostedService>();
            //services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, WriteToFileHostedService2>();
            // services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, ConsumeScopedService>();

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Autor, AutorDTO>();
                configuration.CreateMap<AutorCreacionDTO, Autor>().ReverseMap();
                configuration.CreateMap<Libro, LibroDTO>();
            }, typeof(Startup));

            services.AddScoped<IEjemploLogica, EjemploLogica>();
            //filtro personalizado
            services.AddScoped<MiFiltroDeAccion>();
            //filtro de excepciones
            services.AddScoped<MiFiltroDeExcepcion>();
            //habilita un conjunto de sevicios para almacenar informacion en cache
           services.AddResponseCaching();
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer();
           services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
