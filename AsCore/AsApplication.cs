using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AsCore;

public class AsApplication
{
    public readonly IoServiceManager _serviceManager;
    public IIoServiceFactory? IoServiceFactory { get; set; } = null;
    public ISyncManager? SyncManager{get;set;}
    public ILogger Logger{ get; set; }
    private IConfiguration? _configuration;
    public IoServiceManager IoServiceManager=>_serviceManager;
    public List<IIoService> IoServices => _serviceManager.IoServices;
    public readonly StreamManager StreamManager = new StreamManager();

    public AsApplication(ILogger logger){
        Logger = logger;
        _serviceManager = new IoServiceManager(logger);
    }
    /**
    加载默认配置
    */
    public bool Configure(IConfiguration? configuration)
    {
        Logger.LogInformation("Application configured");
        _configuration = configuration;
        return true;
    }

    /**
    开始采集数据
    */
    public bool StartSample()
    {
        _serviceManager.StartSample();
        return true;
    }
    /**
    停止采集数据
    */
    public bool StopSample()
    {
        _serviceManager.StopSample();
        return true;
    }

    /**
    加载配置信息，如设备设置、通道设置，分析模式等
    */
    public bool LoadProfile(IBundle? profile)
    {
        _serviceManager.LoadProfile(profile, IoServiceFactory!);

        if (IoServices.Count == 0)
        {
            Logger.LogInformation("No io service loaded, use default profile!");
            _serviceManager.LoadDefault(IoServiceFactory!);
        }
        StreamManager.OnIoServicesUpdated(this);
        return true;
    }

    public bool SaveProfile(IBundle profile){
        _serviceManager.SaveProfile(profile);
        return true;
    }

    public bool StartRecordAndAnalysis(){
        return true;
    }

    public bool StopRecordAndAnalysis(){
        return true;
    }

    public bool PauseRecordAndAnalysis(){
        return false;
    }

    public bool ResumeRecordAndAnalysis(){
        return false;
    }
}