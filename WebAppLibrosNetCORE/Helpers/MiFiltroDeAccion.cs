using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.Helpers
{
    public class MiFiltroDeAccion : IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> logger;

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger)
        {
            this.logger = logger;
        }
        //FILTRO DESPUES DE LA ACCION
        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.logger.LogError("OnActionExcecuted, despues de la accion");
        }

        //FILTRO ANTES DE LA ACCION
        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.logger.LogError("OnActionExcecuting, antes de la accion");
        }
    }
}
