using ModuloPruebas.entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModuloPruebas.services
{
    interface IServicioValidacionesDeTransferencias
    {
        string RealizarValidaciones(Cuenta origen, Cuenta destino, decimal montoATransferir);
    }
}
