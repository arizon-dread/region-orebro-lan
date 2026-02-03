using clientside.backend.DIHelper;
using RolDbContext;
using viewmodels;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class SyncService
    {
        private readonly SettingsService settingsService;
        private readonly OrderService orderService;
        private readonly InfoService infoService;

        public SyncService(SettingsService settings, OrderService order, InfoService info)
        {
            settingsService = settings;
            orderService = order;
            infoService = info;
        }
        public async Task<bool> Synchronize()
        {
            await SynchronizeFromServer();
            await SynchronizeToServer();
            return true;
        }
        private async Task<bool> SynchronizeToServer()
        {
            var infos = infoService.GetSavedLocal();
            if (!infos.Any()) return true;
            var serverAddress = settingsService.GetByKey("ServerAddress")?.Value;
            var lastSync = settingsService.GetByKey("LastSync");
            if (!Uri.TryCreate(serverAddress, UriKind.Absolute, out var url)) return false;
            using var client = new HttpClient { BaseAddress = url };
            try
            {
                foreach (var info in infos)
                {
                    var apa = await client.PostAsJsonAsync("api/v1/info", info);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> SynchronizeFromServer()
        {
            var serverAddress = settingsService.GetByKey("ServerAddress")?.Value;
            var lastSync = settingsService.GetByKey("LastSync");
            if (!Uri.TryCreate(serverAddress, UriKind.Absolute, out var url)) return false;
            if (!DateTime.TryParse(lastSync?.Value, out var date)) return false;
            using var client = new HttpClient { BaseAddress = url };
            var now = DateTime.UtcNow;
            try
            {
                var data = await client.GetFromJsonAsync<viewmodels.SyncViewModel>($"api/v1/sync/changed/{date}");
                lastSync.Value = now.ToString();
                settingsService.Save(lastSync);
                foreach (var item in data.Info)
                {
                    item.Status = Status.SavedRemote;
                    infoService.Save(item);
                }
                foreach (var item in data.Order)
                {
                    item.Status = Status.SavedRemote;
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
