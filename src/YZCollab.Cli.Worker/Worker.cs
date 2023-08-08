using System.Net.Http.Json;

namespace YZCollab.Cli.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private record HookRequestDTO(string Message);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var httpClient = new HttpClient();
            var urlHook = _config.GetValue<string>("UrlHook");

            while (!stoppingToken.IsCancellationRequested)
            {
                var msg = new HookRequestDTO($"Serviço s-{new Random().Next()} executando às: {DateTimeOffset.Now:g}");
                await httpClient.PostAsJsonAsync(urlHook, msg, stoppingToken);

                _logger.LogInformation(msg.Message);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}