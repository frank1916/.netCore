﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PruebasDeIntegracion.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAppLibrosNetCORE;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Services;

namespace PruebasDeIntegracion
{
    [TestClass]
    public class AutoresControllerTests
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        public WebApplicationFactory<Startup> ConstruirWebHostBuilderConfigurado()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IRepositorioAutores, RepositorioAutoresMock>();
                   // services.AddSingleton<IAuthorizationHandler, SaltarseRequerimientosHandle>();

                    services.AddControllers(options =>
                    {
                     //   options.Filters.Add(new UsuarioFalsoFilter());
                    });
                });
            });
        }

        [TestMethod]
        public async Task Get_SiElAutorNoExiste_Retorna404()
        {
            var client = ConstruirWebHostBuilderConfigurado().CreateClient();

            var url = "/api/autores/0";
            var response = await client.GetAsync(url);

            Assert.AreEqual(expected: 404, actual: (int)response.StatusCode);
        }

        [TestMethod]
        public async Task Get_SiElAutorExiste_EntoncesLoDevuelve()
        {
            var client = ConstruirWebHostBuilderConfigurado().CreateClient();

            var url = "/api/autores/1";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + response.StatusCode);
            }

            var result = JsonConvert.DeserializeObject<Autor>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: 1, actual: result.id);
            Assert.AreEqual(expected: "Pepito pruebas", actual: result.nombre);
        }
    }
}
