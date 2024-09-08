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
    public string Id{get;set;} = String.Empty;
    private readonly SyncManager SyncManager;
    private readonly DemoIoDevice _device;
    private readonly ILogger _logger;

    public string FullId{get{
        return $"{Id}";
    }}

    public DemoIoService(ILogger logger, SyncManager syncManager){
        _logger = logger;
        SyncManager = syncManager;
        _device = new DemoIoDevice(this, logger, syncManager){
            Id = "1"
        };
    }
    public bool Configure(IConfiguration? configuration)
    { 
        _logger.LogInformation("Configure demo io service");
        _device.Configure(null);
        return true;
    }

    public bool LoadProfile(IBundle? configuration){
        if(configuration!=null){
            var guid = configuration.GetString("guid");
            if(guid != Guid){
                throw new ArgumentException("Guid is not correct in DemoIoService while loading profile");
            }
            var name = configuration.GetString("Name");
            if(name != null){
                Name = name;
            }
            _device.LoadProfile(configuration.GetBundle("device"));
        }
        return true;
    }
    public bool SaveProfile(IBundle configuration){
        configuration.PutString("guid", Guid);
        configuration.PutString("name", Name);
        var bundle = configuration.CreateBundle();
        _device.SaveProfile(bundle);
        configuration.PutBundle("device", bundle);
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
    public List<IDataAdapter> GetRawInputAdapters(){
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