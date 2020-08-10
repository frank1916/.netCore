using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAppLibrosNetCORE.Context;
using WebAppLibrosNetCORE.Entities;

namespace WebAppLibrosNetCORE.Services
{
    public class ConsumeScopedService : IHostedService, IDisposable
    {

        private Timer timer;
        public IServiceProvider Services { get; }

        public ConsumeScopedService(IServiceProvider service)
        {
            Services = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(doWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private void doWork(Object state)
        {
           using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
                var message = "Consumiendoservicio recibido mensaje en " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                var log = new Autor() {nombre = message };
                context.Autores.Add(log);
                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}
