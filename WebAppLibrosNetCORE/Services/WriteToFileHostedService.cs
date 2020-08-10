using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppLibrosNetCORE.Services
{
    public class WriteToFileHostedService : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment environment;
        private readonly string fileName = "File 1.txt";
        private Timer timer;

        public WriteToFileHostedService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        //tarea que se ejecuta cuando inicia la aplicacion 
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.writeToFile("escribiento por IHOSTEDSERVICE: aplicacion iniciada");
            this.timer = new Timer(doWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(4));
            return Task.CompletedTask;
        }

        private void doWork (Object state)
        {
            writeToFile("escribiendo por tarea de trabajo cada : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        //tarea que se ejecuta cuando termina la aplicacion
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.writeToFile("escribiento por IHOSTEDSERVICE: aplicacion terminada");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;

        }

        private void writeToFile(string message)
        {
            var path = $@"{this.environment.ContentRootPath}\wwwroot\{fileName}";
            using (StreamWriter writer  = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }

        //permite limpiar los recursos del  timer
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
