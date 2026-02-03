
using clientside.backend.Settings;
using System.Diagnostics;

namespace clientside.backend.Service
{
    public class SyncService : IHostedService
    {
        private Timer? _timer;
        public SyncService(ServiceSettings serviceSettings) 
        {
            
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ => await OnTimerFiredAsync(cancellationToken),
            null, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(1));
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
                // do your work here
                Debug.WriteLine($"Syncing {DateTime.Now}");
            }
            finally
            {
                _timer?.Change(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(1));
            }
        }
    }
}
