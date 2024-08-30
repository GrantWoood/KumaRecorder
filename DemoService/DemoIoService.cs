using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
namespace DemoService;

public class DemoIoService: IIoService
{
    private readonly SyncManager SyncManager;
    private readonly DemoIoDevice _device;
    private readonly ILogger _logger;

    public DemoIoService(ILogger logger, SyncManager syncManager){
        _logger = logger;
        SyncManager = syncManager;
        _device = new DemoIoDevice(logger, syncManager);
    }
    public bool Configure(IConfiguration? configuration)
    {
        _logger.LogInformation("Configure demo io service");
        _device.Configure(null);
        return true;
    }
    public List<IIoDevice> GetIoDevices(){
        return [_device];
    }
    public List<IIoChannel> GetIoChannels(){
        return _device.GetIoChannels();
    }
    public List<IDataAdapter> GetInputAdapters(){
        return _device.GetInputAdapters();
    }

    public bool StartSample()
    {
        _logger.LogInformation("Demo io service is running");
        _device.StartSample();
        return true;
    }

    public bool StopSample()
    {
        _device.StopSample();
        _logger.LogInformation("Demo io service stopped");
        return true;
    }
}