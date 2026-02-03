
using System.Diagnostics;

namespace clientside.backend.Service
{
    public class PollService( IServiceProvider serviceProvider) : IHostedService
    {
        private Timer? _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
            null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            
            return Task.CompletedTask;
        }
        private async Task OnTimerFiredAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var syncService = scope.ServiceProvider.GetRequiredService<SyncService>();
                var synced = await syncService.Synchronize();
                // do your work here
                Debug.WriteLine($"Syncing {DateTime.Now}. Last Sync {synced}");
            }
            finally
            {
                _timer?.Change(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(1));
            }
        }
    }
}
