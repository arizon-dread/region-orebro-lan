using clientside.backend.DIHelper;
using RolDbContext;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class SyncService(RolEfContext context)
    {

        public async Task<bool> Synchronize()
        {
            var serverAddress = context.ApplicationStatus.FirstOrDefault(d => d.Key == "ServerAddress")?.Value;
            var lastSync = context.ApplicationStatus.FirstOrDefault(d => d.Key == "LastSync");
            if(!Uri.TryCreate(serverAddress, UriKind.Absolute, out var url)) return false;
            if(!DateTime.TryParse(lastSync?.Value, out var date)) return false;
            var client = new HttpClient {BaseAddress = url};
            try
            {
                var data = await client.GetFromJsonAsync<viewmodels.SyncViewModel>($"api/sync/changed/{date}");
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
