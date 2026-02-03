using clientside.backend.DIHelper;
using RolDbContext;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class SyncService(SettingsService settingsService, OrderService orderService, InfoService infoService)
    {

        public async Task<bool> Synchronize()
        {
            var serverAddress = settingsService.GetByKey("ServerAddress")?.Value;
            var lastSync = settingsService.GetByKey("LastSync");
            if(!Uri.TryCreate(serverAddress, UriKind.Absolute, out var url)) return false;
            if(!DateTime.TryParse(lastSync?.Value, out var date)) return false;
            var client = new HttpClient {BaseAddress = url};
            var now = DateTime.UtcNow;
            try
            {
                var data = await client.GetFromJsonAsync<viewmodels.SyncViewModel>($"api/v1/sync/changed/{date}");
                lastSync.Value = now.ToString();
                settingsService.Save(lastSync);
                foreach (var item in data.Info)
                {
                    infoService.Save(item);
                }
                foreach (var item in data.Order)
                {
                    orderService.Save(item);
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
