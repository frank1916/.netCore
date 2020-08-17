using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebAppLibrosNetCORE;

namespace PruebasDeIntegracion
{
    [TestClass]
    public class ValuesControllerTest
    {
        private WebApplicationFactory<Startup> factory ;

        //permite correr codigo  antes de ejecutar pruebas
        [TestInitialize]
        public void Inicialize()
        {
            this.factory = new WebApplicationFactory<Startup>();

        }

        [TestMethod]
        public async Task GET_devuelve_arreglo_de_dos_elementos()
        {
            var client = this.factory.CreateClient();
            var url = "api/values";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "codigo de estatus no eistoso: " + response.StatusCode);
            }
            var result = JsonConvert.DeserializeObject<string[]>(
                await response.Content.ReadAsStringAsync());

            Assert.AreEqual(expected: 2, actual: result.Length);
        }
    }
}
