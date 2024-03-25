namespace WorkerServiceExample
{
    public sealed class DefaultScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger<DefaultScopedProcessingService> logger;
        private int _executionCount;

        public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger)
        {
            this.logger = logger;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ++_executionCount;
                logger.LogInformation(
                    "{ServiceName} working, execution count: {Count}",
                    nameof(DefaultScopedProcessingService),
                    _executionCount);

                await Task.Delay(1_000, stoppingToken);
            }
        }
    }
}
