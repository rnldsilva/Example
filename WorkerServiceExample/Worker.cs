namespace WorkerServiceExample
{
    public sealed class ScopedBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ScopedBackgroundService> _logger;
        private const string ClassName = nameof(ScopedBackgroundService);

        public ScopedBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<ScopedBackgroundService> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{Name} is working.", ClassName);

            using (IServiceScope scope = this.serviceScopeFactory.CreateScope())
            {
                IScopedProcessingService scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWorkAsync(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{Name} is stopping.", ClassName);

            await base.StopAsync(stoppingToken);
        }
    }
}