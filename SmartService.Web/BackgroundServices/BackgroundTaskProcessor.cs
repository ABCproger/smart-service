namespace SmartService.BackgroundServices;

public class BackgroundTaskProcessor : BackgroundService
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<BackgroundTaskProcessor> _logger;

    public BackgroundTaskProcessor(IBackgroundTaskQueue taskQueue, ILogger<BackgroundTaskProcessor> logger)
    {
        _taskQueue = taskQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Task Processor is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await _taskQueue.DequeueAsync(stoppingToken);

            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing background task.");
            }
        }

        _logger.LogInformation("Background Task Processor is stopping.");
    }
}