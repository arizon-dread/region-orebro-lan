

namespace clientside.backend.Service
{
    public class PollService(IServiceProvider serviceProvider) : IHostedService
    {
        private Timer? _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
            null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();

            return Task.CompletedTask;
        }
        private async Task OnTimerFiredAsync(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine($"Syncing starting {DateTime.Now}");
                using var scope = serviceProvider.CreateScope();
                var syncService = scope.ServiceProvider.GetRequiredService<SyncService>();
                var synced = await syncService.Synchronize();
                foreach (var item in synced)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine($"Syncing done {DateTime.Now}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Syncing failed {ex.Message}");
            }
        }
    }
}
