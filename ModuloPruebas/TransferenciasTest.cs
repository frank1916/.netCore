using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuloPruebas.entidades;
using ModuloPruebas.services;
using Moq;
using System;

namespace ModuloPruebas
{
    [TestClass]
    public class TransferenciasTest
    {
        [TestMethod]
        public void TransferenciaEntreCuentasConFondosInsuficientesArrojaUnError()
        {
            // Preparaci�n
            Exception expectedException = null;
            Cuenta origen = new Cuenta() { Fondos = 10 };
            Cuenta destino = new Cuenta() { Fondos = 5 };
            decimal montoATransferir = 7m;
            var mock = new Mock<IServicioValidacionesDeTransferencias>();

            string mensajeError = " mensaje de error";
            mock.Setup(x => x.RealizarValidaciones(origen, destino, montoATransferir)).Returns(mensajeError);



            var servicio = new ServicioDeTransferenciasSinMocks();

            // Prueba
            try
            {
                servicio.TransferirEntreCuentas(origen, destino, montoATransferir);
                Assert.Fail("Un error debi� ser arrojado");
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Verificaci�n
            //Assert.IsTrue(expectedException is ApplicationException);
            //Assert.AreEqual("La cuenta origen no tiene fondos suficientes para realizar la operaci�n", expectedException.Message);
            Assert.AreEqual(3, origen.Fondos);
            Assert.AreEqual(12, destino.Fondos);
        }
    }
}
