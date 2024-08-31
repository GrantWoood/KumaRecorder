using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AsCore;

public class AsApplication
{
    private readonly List<IIoService> _ioServices = [];
    public IIoServiceFactory? IoServiceFactory { get; set; } = null;
    public List<IIoService> IoServices => _ioServices;
    public ISyncManager? SyncManager{get;set;}
    public required ILogger Logger{ get; set; }
    private IConfiguration? _configuration;
    private IConfiguration? _profile;

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
        foreach (var service in _ioServices)
        {
            service.StartSample();
        }
        return false;
    }
    /**
    停止采集数据
    */
    public bool StopSample()
    {
        foreach (var service in _ioServices)
        {
            service.StopSample();
        }
        return false;
    }

    /**
    加载配置信息，如设备设置、通道设置，分析模式等
    */
    public bool LoadProfile(IConfiguration? profile){
        if(profile == null){
            //Load Demo
            Logger.LogInformation("Profile isn't provided, use default profile!");
            var service = IoServiceFactory!.Create("");
            if (service != null)
            {
                service.Configure(null);
                _ioServices.Add(service);
            }
        }
        else
        {
            var servicesProfile = profile.GetSection("IoServices");
            foreach (var sp in servicesProfile.GetChildren())
            {
                var sId = sp["Id"];
                var service = IoServiceFactory!.Create(sId);
                if (service == null)
                {
                    Logger.LogError($"Profile is provided, but io service create failed for {sId}");
                    //Service created failed
                }
                else if (!service.LoadProfile(sp))
                {
                    Logger.LogError($"Load profile failed for io service {service.Name}!");
                }
                else
                {
                    Logger.LogInformation($"Profile load success for {service.Name}!");
                }
            }
        }

        return true;
    }

}