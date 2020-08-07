using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerLifetime
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("Log triggered from cancellation token in Worker");
                Console.WriteLine("Console.WriteLine triggered from cancellation token in Worker");
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(6000, stoppingToken);
            }

            // When would these run??
            _logger.LogInformation("Log fired at the end of ExecuteAsync in Worker");
            Console.WriteLine("Console.WriteLine fired at the end of ExecuteAsync in Worker");
        }
    }
}
