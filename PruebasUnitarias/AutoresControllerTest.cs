using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAppLibrosNetCORE.Controllers;
using WebAppLibrosNetCORE.Entities;
using WebAppLibrosNetCORE.Services;

namespace PruebasUnitarias
{
    [TestClass]
    public class AutoresControllerTest
    {
        [TestMethod]
        public void GetSiElAutorNoExiste_Retorna404 ()
        {
            //preparacion
            var idAutor = 1;
            var mock = new Mock<IRepositorioAutores>();
            mock.Setup(x => x.ObtenerPorId(idAutor)).Returns(default(Autor));
            var autoreController =  new PruebaController(mock.Object);

            //prueba

            var resultado = autoreController.Get(idAutor);

            //verificacion
            Assert.IsInstanceOfType(resultado.Result, typeof(NotFoundResult));

        }


        [TestMethod]
        public void GetSiElAutorExiste_RetornaAutor()
        {
            //preparacion
            var autor = new Autor() { id = 1, nombre = "francisco"};

            var mock = new Mock<IRepositorioAutores>();
            mock.Setup(x => x.ObtenerPorId(autor.id)).Returns(autor);
            var autoreController = new PruebaController(mock.Object);

            //prueba

            var resultado = autoreController.Get(autor.id);

            //verificacion
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(resultado.Value.id, autor.id);
            Assert.AreEqual(resultado.Value.nombre, autor.nombre);

        }

    }
}
