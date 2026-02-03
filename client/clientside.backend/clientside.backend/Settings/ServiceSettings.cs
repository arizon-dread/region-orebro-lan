
namespace clientside.backend.Settings;

public class ServiceSettings
{
    public string Mode { get; set; }
    public string RemoteServer { get; set; }
    public bool IsServer => Mode == "Server";
    public bool IsClient => Mode == "Client";
}
