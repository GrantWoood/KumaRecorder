using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
namespace DemoService;

public class DemoIoService: IIoService
{
    static public string Guid = "04C79BB3-6966-4BC9-9EA3-B20127A5F241";
    public string Name{get;set;} = "Demo Io Service";
    public string Id{
        get{
            return Guid;
        }
    }
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

    public bool LoadProfile(IConfiguration? configuration){
        return false;
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