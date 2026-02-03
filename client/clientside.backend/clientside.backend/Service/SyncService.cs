
using clientside.backend.Settings;
using System.Diagnostics;

namespace clientside.backend.Service
{
    public class SyncService( IServiceProvider serviceProvider) : IHostedService
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
                var settingsService = scope.ServiceProvider.GetRequiredService<SettingsService>();
                var settings = settingsService.GetByKey("LastSync");
                // do your work here
                Debug.WriteLine($"Syncing {DateTime.Now}. Last Sync {settings?.Value}");
            }
            finally
            {
                _timer?.Change(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(1));
            }
        }
    }
}
