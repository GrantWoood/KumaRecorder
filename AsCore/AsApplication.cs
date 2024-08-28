using AsAbstract;
using AsBasic;

namespace AsCore;

public class AsApplication
{
    private readonly List<IIoService> _ioServices = [];
    public IIoServiceFactory? IoServiceFactory { get; set; } = null;
    public List<IIoService> IoServices => _ioServices;
    public ISyncManager? SyncManager{get;set;}

    public bool Configure(IConfiguration? configuration)
    {
        var service = IoServiceFactory!.Create("");
        if (service != null)
        {
            service.Configure(null);
            _ioServices.Add(service);
        }
        return false;
    }

    public IConfiguration GetConfiguration(){
        AsConfiguration configuration = new AsConfiguration();
        return configuration;
    }

    public bool StartSample()
    {
        foreach (var service in _ioServices)
        {
            service.StartSample();
        }
        return false;
    }

    public bool StopSample()
    {
        foreach (var service in _ioServices)
        {
            service.StopSample();
        }
        return false;
    }
}