using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebAppLibrosNetCORE.Context;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Helpers;
using WebAppLibrosNetCORE.ModelsDTO;
using WebAppLibrosNetCORE.Services;

//convencion para que en swagger se muestren todos los tipos de codigos de retorno del controlador
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

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

            //filtro personalizado
            services.AddScoped<MiFiltroDeAccion>();
            //filtro de excepciones
            services.AddScoped<MiFiltroDeExcepcion>();
            //filtro de hateos
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<HATEOASAuthorFilterAttribute>();
            services.AddScoped<HATEOASFilterAttribute>();
            services.AddScoped<GeneradorEnlaces>();

            

            //habilita un conjunto de sevicios para almacenar informacion en cache
           services.AddResponseCaching();
           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer();
           services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            //configurar Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Mi Web API",
                    Description = "Descripcion del API",
                    TermsOfService = new Uri("https://www.udemy.com/user/frankParra/"),
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("http://bfy.tw/4nqh")
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Felipe Gavilán",
                        Email = "felipe_gavilan887@hotmail.com",
                        Url = new Uri("https://gavilan.blog/")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(config =>
           {
               config.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Web API");

           });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
