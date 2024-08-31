using AsAbstract;
using AsBasic;
using Microsoft.Extensions.Configuration;

namespace AsCore;

public class AsApplication
{
    private readonly List<IIoService> _ioServices = [];
    public IIoServiceFactory? IoServiceFactory { get; set; } = null;
    public List<IIoService> IoServices => _ioServices;
    public ISyncManager? SyncManager{get;set;}

    /**
    加载默认配置
    */
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
    public bool LoadProfile(){
        return true;
    }
}