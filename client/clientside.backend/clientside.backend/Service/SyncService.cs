using clientside.backend.DIHelper;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        public async Task<List<string>> Synchronize()
        {
            var sync2 = await SynchronizeToServer();
            var sync1 = await SynchronizeFromServer();
            return [.. sync1, .. sync2];
        }
        private async Task<List<string>> SynchronizeToServer()
        {
            var infos = infoService.GetSavedLocal();
            var result = new List<string>
            {
                "* SynchronizeToServer",
                $"Number of localy saved: {infos.Count()}"
            };
            if (!infos.Any()) return result;
            var serverAddress = settingsService.GetByKey("ServerAddress")?.Value;
            if (!Uri.TryCreate(serverAddress, UriKind.Absolute, out var url))
            {
                result.Add($"Invalid serveraddress: {url}");
                return result;
            }
            using var client = new HttpClient { BaseAddress = url };
            try
            {
                foreach (var info in infos)
                {
                    var apa = await client.PostAsJsonAsync("api/v1/info", info);
                    result.Add($"Save info remote {info.Id}. Http-Status {apa.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }

            return result;
        }

        private async Task<List<string>> SynchronizeFromServer()
        {
            var results = new List<string>
            {
                "* SynchronizeFromServer"
            };
            var lastSync = settingsService.GetByKey("LastSync");
            var serverAddress = settingsService.GetByKey("ServerAddress")?.Value;
            if (!Uri.TryCreate(serverAddress, UriKind.Absolute, out var url))
            {
                results.Add($"Invalid serveraddress: {url}");
                return results;
            }
            if (!DateTime.TryParse(lastSync?.Value, out var date)) return ["Invalid syncdate"];
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
                    var s = infoService.Save(item);
                    results.Add($"Saved info {item.Id}. Status {s.Status}");
                }
                foreach (var item in data.Order)
                {
                    item.Status = Status.SavedRemote;
                    var s = orderService.Save(item);
                    results.Add($"Saved info {item.Id}. Status {s.Status}");
                }

            }
            catch (Exception ex)
            {
                results.Add(ex.Message);
            }
            return results;
        }
    }
}
